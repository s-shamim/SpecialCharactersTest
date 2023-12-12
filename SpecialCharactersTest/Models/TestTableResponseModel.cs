using System.ComponentModel.DataAnnotations;
using SpecialCharactersTest.Common;

namespace SpecialCharactersTest.Models
{
    public class TestTableResponseModel
    {
        public int Id { get; set; }

        public string TestColumnMedium { get; set; }

        public string TestColumnLong { get; set; }

        public bool TestColumnBit { get; set; }

        public DateTime TestColumnDate { get; set; }

        public DateTime TestColumnDateTime { get; set; }
    }
    public class TestTableRequestModel
    {
        [MaxLength(50), RegularExpression(Constants.RegularExpression)]
        public string? TestColumnMedium { get; set; }

        [MaxLength(1000), RegularExpression(Constants.RegularExpressionWithPercentAndUnderscore)]
        public string? TestColumnLong { get; set; }

        public bool TestColumnBit { get; set; }

        public DateTime TestColumnDate { get; set; }

        public DateTime TestColumnDateTime { get; set; }
    }

}
