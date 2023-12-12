using System.Data;
using System.Reflection;

namespace SpecialCharactersTest.Data
{
    public static class DataTableToListMapper
    {
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                T objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo? pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));

                    }
                }
                return objT;
            }).ToList();
        }

        // Convert to Data Table
        public static DataTable ConvertToDataTable<T>(List<T> dataList)
        {
            DataTable convertedTable = new DataTable();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in propertyInfo)
            {
                convertedTable.Columns.Add(prop.Name);
            }
            foreach (T item in dataList)
            {
                DataRow row = convertedTable.NewRow();
                var values = new object[propertyInfo.Length];
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    object? test = propertyInfo[i].GetValue(item, null);
                    row[i] = propertyInfo[i].GetValue(item, null);
                }
                convertedTable.Rows.Add(row);
            }
            return convertedTable;
        }
    }
}
