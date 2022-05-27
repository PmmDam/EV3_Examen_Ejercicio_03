using EV3_Examen_Ejercicio_03.Model;
using Microsoft.Data.SqlClient;
using MyLib.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3_Examen_Ejercicio_03.Manager
{
    public class ProviderManager : IProviderManager
    {
        SqlServer myDb { get; set; }
        public ProviderManager(SqlServer myDb)
        {
            this.myDb = myDb;
        }


        // Procedimiento almacenado utilizado

        //        SET ANSI_NULLS ON
        //GO
        //SET QUOTED_IDENTIFIER ON
        //GO

        //CREATE PROCEDURE ListAllSuppliers(@Id int)
        //AS
        //BEGIN


        //    SET NOCOUNT ON;
        //SELECT* from Providers
        //WHERE Id=(SELECT Id from Products_Provider
        //WHERE IdProduct = (SELECT IdProduct FROM Products WHERE Name = @Name));
        //END
        //GO


        public List<ProviderModel> ProviderList { get; set; }

        public async Task GetAllProvidersFromProductAsync(string Name)
        {

            SqlParameter NameProductParameter = GetSqlParameter(Name);
            await this.myDb.ExecuteReaderAsync("ListAllSuppliers", CommandType.StoredProcedure, ReadAllProviders, NameProductParameter);

            CheckNames(this.ProviderList, Name);

        }



        private async Task ReadAllProviders(SqlDataReader reader)
        {

            this.ProviderList = new List<ProviderModel>();
            if (reader.HasRows)
            {

                while (await reader.ReadAsync())
                {

                    ProviderModel provider = new ProviderModel();

                    if (!reader.IsDBNull(0))
                    {
                        provider.IdProvider = reader.GetInt32(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        provider.Name = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        provider.Adress = reader.GetString(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        provider.Phone = reader.GetString(3);
                    }

                    if (provider != null)
                        this.ProviderList.Add(provider);
                }
            }

        }

        private void CheckNames(List<ProviderModel> providersList, string Name)
        {
            for (int i = 0; i < providersList.Count; i++)
            {
                if (!ProviderList[i].Name.Contains(Name))
                {
                    ProviderList.RemoveAt(i);
                }
            }
        }

        private SqlParameter GetSqlParameter(string Name)
        {
            SqlParameter NameParameter = new SqlParameter();

            NameParameter.ParameterName = "@Name";
            NameParameter.Value = Name;
            NameParameter.DbType = DbType.String;
            NameParameter.Direction = ParameterDirection.Input;
            return NameParameter;
        }




    }
}
