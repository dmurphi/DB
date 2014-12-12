using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triemli
{
    public class CrudOps
    {
        #region member variables

        private static PetaPoco.Database _currentDb;

        #endregion

        #region constructors

        public CrudOps(PetaPoco.Database _db)
        {
            _currentDb = _db;
        }

        #endregion


        #region db operations
        ////// use parameterised queries like this:
        //////        db.Query<MyTable>("WHERE MyColumn=@myValue", new {myValue = "test"})
        ////// for better performance


        public object GetData(string select, string from, string whereConditions)
        {
            var sql = PetaPoco.Sql.Builder
                                            .Select("SELECT @0", select)
                                            .From("@0",from)
                                            .Where("@0", whereConditions);

            var product = _currentDb.Fetch<object>(sql);
            return product;
        }


        public int UpdateData(object pocoObject)
        {
            return _currentDb.Update(pocoObject);
        }


        public int DeleteData(object pocoObject)
        {
            return _currentDb.Delete(pocoObject);
        }

        public void Insert(object pocoObject)
        {
            _currentDb.Insert(pocoObject);
        }


        #endregion

    }
}