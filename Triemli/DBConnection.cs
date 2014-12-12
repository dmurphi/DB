using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triemli
{
    public class DBConnection
    {
        #region member variables
        
        private PetaPoco.Database _db;
        private static string _connectionString;
        private static string _provider;
        
        #endregion


        #region constructors

        public DBConnection(string connString)
        {
            _connectionString = connString;
            if (_db == null || _db.Connection.ConnectionString.Trim().ToLower() != _connectionString.Trim().ToLower())
            {
                _db = new PetaPoco.Database(_connectionString);
            }
        }

        public DBConnection(string connString, string provider)
        {
            _connectionString = connString;
            _provider = provider;
            if (_db == null || _db.Connection.ConnectionString.Trim().ToLower() != _connectionString.Trim().ToLower())
            {
                _db = new PetaPoco.Database(_connectionString, _provider);
            }
        }

        #endregion

        #region public methods

        public PetaPoco.Database currentDatabase
        {
            get
            {
                return _db;
            }
        }

        #endregion
    }
}