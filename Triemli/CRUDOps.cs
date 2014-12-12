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

        #region SELECT

        public object GetScalar(string select, string from, string whereConditions)
        {
            var sql = PetaPoco.Sql.Builder
                                            .Select("SELECT @0", select)
                                            .From("@0", from)
                                            .Where("@0", whereConditions);

            return _currentDb.ExecuteScalar<object>(sql);
        }


        public object GetSingleOrDefault(string select, string from, string whereConditions)
        {
            var sql = PetaPoco.Sql.Builder
                                            .Select("SELECT @0", select)
                                            .From("@0", from);
            
            if (!String.IsNullOrEmpty(whereConditions))
            {

                sql
                    .Where("@0", whereConditions);
            }
            
            return _currentDb.FirstOrDefault<object>(sql);
        }


        public PetaPoco.Page<object> GetPaged(string select, string from, string whereConditions,int pageNumber, int itemsPerPage)
        {
            var sql = PetaPoco.Sql.Builder
                                            .Select("SELECT @0", select)
                                            .From("@0", from);

            if (!String.IsNullOrEmpty(whereConditions))
            {

                sql
                    .Where("@0", whereConditions);
            }

            return _currentDb.Page<object>(pageNumber, itemsPerPage, sql);
        }

        public object GetData(string select, string from, string whereConditions)
        {
            var sql = PetaPoco.Sql.Builder
                                            .Select("SELECT @0", select)
                                            .From("@0",from)
                                            .Where("@0", whereConditions);

            var product = _currentDb.Fetch<object>(sql);
            return product;
        }

        #endregion

        #region UPDATE
        
        public int UpdateData(object pocoObject)
        {
            return _currentDb.Update(pocoObject);
        }
        
        #endregion

        #region DELETE
        
        public int DeleteData(object pocoObject)
        {
            return _currentDb.Delete(pocoObject);
        }
        
        #endregion

        #region INSERT
        
        public void Insert(object pocoObject)
        {
            _currentDb.Insert(pocoObject);
        }
        
        #endregion


        #endregion

    }
}