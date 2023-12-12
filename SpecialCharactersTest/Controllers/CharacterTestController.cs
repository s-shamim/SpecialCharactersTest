using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SpecialCharactersTest.Common;
using SpecialCharactersTest.Data;
using SpecialCharactersTest.Models;
using System.ComponentModel.DataAnnotations;

namespace SpecialCharactersTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class  CharacterTestController:ControllerBase
    {
        public SpecialCharactersTestContext _context;
        private readonly IStoredProcedureManager _storedProcedureManager;

        public CharacterTestController(SpecialCharactersTestContext context, IStoredProcedureManager storedProcedureManager)
        {
            _context = context;
            _storedProcedureManager = storedProcedureManager;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_context.TestTables.ToList());
        //}

        [HttpPost]
        public IActionResult PostDataFromQuery([FromQuery]TestTableRequestModel testTableRequestModel)
        {
            var testTable = new TestTable();
            testTable.TestColumnMedium = testTableRequestModel.TestColumnMedium;
            testTable.TestColumnLong = testTableRequestModel.TestColumnLong;
            testTable.TestColumnBit = testTableRequestModel.TestColumnBit;
            testTable.TestColumnDate = DateTime.UtcNow;
            testTable.TestColumnDateTime = DateTime.UtcNow;

            _context.TestTables.Add(testTable);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost] 
        public IActionResult PostData(TestTableRequestModel testTableRequestModel)
        {
            var testTable = new TestTable();
            testTable.TestColumnMedium = testTableRequestModel.TestColumnMedium;
            testTable.TestColumnLong = ReplaceSpecialCharacters.ReplacePercentAndUnderscoreCharacters(testTableRequestModel.TestColumnLong!);
            testTable.TestColumnBit = testTableRequestModel.TestColumnBit;
            testTable.TestColumnDate = testTableRequestModel.TestColumnDate;
            testTable.TestColumnDateTime = testTableRequestModel.TestColumnDateTime;

            _context.TestTables.Add(testTable);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SearchTableDynamic(
            [RegularExpression(Constants.RegularExpressionWithPercentAndUnderscore)]string searchString)
        {
            var sparams = new List<SqlParameter>()
            {
                new SqlParameter() 
                { 
                    ParameterName = "@SearchString", 
                    Value = ReplaceSpecialCharacters.ReplacePercentAndUnderscoreCharacters(searchString), 
                    SqlDbType = System.Data.SqlDbType.NVarChar 
                } 
            };

            var result = await _storedProcedureManager.GetDataFromStoredProcedure("SearchAllTable-Dynamic", sparams);

            List<TestTableResponseModel> list = DataTableToListMapper.ConvertToList<TestTableResponseModel>(result);

            list.ForEach(x => x.TestColumnLong = ReplaceSpecialCharacters.ReplacePercentAndUnderscoreReplacementBackToCharacters(x.TestColumnLong));

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> SearchTableNonDynamic(
            [RegularExpression(Constants.RegularExpressionWithPercentAndUnderscore)] string searchString)
        {
            var sparams = new List<SqlParameter>()
            {
                new SqlParameter() 
                { 
                    ParameterName = "@SearchString", 
                    Value = ReplaceSpecialCharacters.ReplacePercentAndUnderscoreCharacters(searchString),
                    SqlDbType = System.Data.SqlDbType.NVarChar 
                }
            };

            var result = await _storedProcedureManager.GetDataFromStoredProcedure("SearchAllTable-NonDynamic", sparams);

            List<TestTableResponseModel> list = DataTableToListMapper.ConvertToList<TestTableResponseModel>(result);

            list.ForEach(x => x.TestColumnLong = ReplaceSpecialCharacters.ReplacePercentAndUnderscoreReplacementBackToCharacters(x.TestColumnLong));

            return Ok(list);
        }

    }
}
