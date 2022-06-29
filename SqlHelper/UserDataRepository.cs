using System;
using RandomTextCase.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace RandomTextCase.SqlHelper
{
    public class UserDataRepository : IUserDataRepository
    {
        IConnectionsStringHelper _connectionsStringHelper;

        public UserDataRepository(IConnectionsStringHelper connectionsStringHelper)
        {
            _connectionsStringHelper = connectionsStringHelper;
        }

        public List<Input> GetList()
        {
            var connectionString = _connectionsStringHelper.GetConnectionString();

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "SELECT * FROM test.RandomText";
                cmd.Connection = conn;

                var reader = cmd.ExecuteReader();

                List<Input> list = new List<Input>();

                while (reader.Read())
                {
                    Input model = new Input();

                    model.TextID = reader.GetInt32(0);
                    model.inputText = reader.GetString(1);

                    list.Add(model);
                }

                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseViewModel InsertData([Bind("inputText")] Input entity)
        {
            var connectionString = _connectionsStringHelper.GetConnectionString();

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = @"INSERT INTO `test`.`RandomText` (`inputText`) VALUES (@inputText);";
                cmd.Parameters.AddWithValue("inputText", entity.inputText);
                cmd.Connection = conn;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return new ResponseViewModel { Message = "Data inserted.", Success = true };
                }
                else
                {
                    return new ResponseViewModel { Message = "Something went wrong", Success = false };
                }

            }
            catch(Exception ex)
            {
                return new ResponseViewModel { Message = ex.ToString(), Success = false };
            }
            

        }

       public ResponseViewModel UpdateData([Bind("inputText")] Input entity)
        {

            var connectionString = _connectionsStringHelper.GetConnectionString();

            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = connectionString;

            try
            {
                if(conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = @"UPDATE `test`.`RandomText` SET `inputText` = 'sadsa' WHERE (@TextID = 2)";
                cmd.Parameters.AddWithValue("@TextID", entity.inputText);
                cmd.Connection = conn;

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return new ResponseViewModel { Message = "Data inserted.", Success = true };
                }
                else
                {
                    return new ResponseViewModel { Message = "Something went wrong", Success = false };
                }


            }
            catch (Exception ex)
            {
                return new ResponseViewModel { Message = ex.ToString(), Success = false };
            }

        }
    }
}

