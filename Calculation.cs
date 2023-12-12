using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Паспорт_форм.Properties;

namespace Паспорт_форм
{
    class Calculation
    {
        // Актуальный объем черн. +/- 0,25
        /*
            Входной параметр: ID формокомплека
            Выходной: актуальный объем черновых форм (среднее значение между полями [Set_min] и [Set_max] по конкретному ФК за последнюю постановку )
        */
        private string ValueActual(int idfk)
        {
            var conn = DB.GetConnection(Resources.Server, Resources.User, Resources.Password, Resources.secure);
            conn.Open();
            var sql = @"SELECT 
                            (cast(REPLACE(Set_min, ',','.') as float) + cast(REPLACE(Set_max, ',','.') as float)) / 2 as avgval 
                        FROM 
                            [MFU].[dbo].[Volume_ex]
                        WHERE 
                            CONVERT(datetime, [Data_set], 105) = (Select MAX( CONVERT(datetime, [Data_set], 105)) from [MFU].[dbo].[Volume_ex] where N_fk = @idfk)";
            var command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@idfk", idfk);
            return command.ExecuteScalar().ToString();
        }

        // Фактическая вместимость по уровню
        /*
            Входной параметр: ID формокомплека
            Выходной:  Фактическая вместимость по уровню (среднее значение между тремя последними значениями пересчитанной вместимости. Поле Volume таблицы [MFU].[dbo].[Volume_mess])
        */
        private string factVmest(int idfk)
        {
            var conn = DB.GetConnection(Resources.Server, Resources.User, Resources.Password, Resources.secure);
            conn.Open();
            var sql = @"SELECT round(avg (a.Volume), 2)
                        FROM 
                            (
                                SELECT TOP(3) cast(REPLACE(Volume, ',','.') as float) as Volume FROM [MFU].[dbo].[Volume_mess] where Number_fk = @idfk ORDER BY Date desc
                            ) a";
            var command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@idfk", idfk);
            return command.ExecuteScalar().ToString();
        }
    }
}
