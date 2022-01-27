using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDantist.FactorySqlQuery
{
    /// <summary>
    /// Формирует SQl запрос для отчета Информация по стоматологиям.
    /// </summary>
    public class QuerySqlReportInformStomatolog : ICreateSqlQuery
    {
        private string strDateStart = string.Empty;
        private string strDateEnd = string.Empty;

        public QuerySqlReportInformStomatolog(string strDateStart, string strDateEnd)
        {
            this.strDateStart = strDateStart ?? throw new ArgumentNullException(nameof(strDateStart));
            this.strDateEnd = strDateEnd ?? throw new ArgumentNullException(nameof(strDateEnd));
        }

        /// <summary>
        /// Строка sql запроса.
        /// </summary>
        /// <returns></returns>
        public string SqlQuery()
        {
            string query = @"
                        SELECT TabNameHospital.Район, TabNameHospital.Поликлинника, TabNameHospital.Пропускная_способность_за_год as 'Пропускная способность за год', TabNameHospital.ИНН, TabContractValid.количество_заключенных_договоров as 'количество заключенных договоров',
                        TabContractValid.сумма_заключенных_договоров as 'сумма заключенных договоров', TabContractPayment.количество_договоров_находящихся_в_деле as 'количество договоров находящихся в деле',TabContractPayment.сумма_договоров_находящихся_в_деле as 'сумма договоров находящихся в деле', 
                        TabAct.количество_договоров_поступивших_на_оплату as 'количество договоров поступивших на оплату', TabAct.сумма_договоро_поступивщих_на_оплату as 'сумма договоро поступивщих на оплату', TabNameHospital.SerialNumber
                        FROM(SELECT        dbo.РайонОбласти.NameRegion AS 'Район', dbo.ПоликлинникиИнн.F2 AS 'Поликлинника', dbo.ПоликлинникиИнн.LimitYearPiple AS 'Пропускная_способность_за_год', dbo.ПоликлинникиИнн.F3 AS 'ИНН',
                                                    dbo.ПоликлинникиИнн.SerialNumber
                          FROM            dbo.РайонОбласти

                          RIGHT OUTER JOIN
                          dbo.ПоликлинникиИнн ON dbo.ПоликлинникиИнн.idRegion = dbo.РайонОбласти.idRegion
                          GROUP BY dbo.ПоликлинникиИнн.F2, dbo.ПоликлинникиИнн.LimitYearPiple, dbo.РайонОбласти.NameRegion, dbo.ПоликлинникиИнн.F3, dbo.ПоликлинникиИнн.SerialNumber) AS TabNameHospital

                          LEFT OUTER JOIN
                             (SELECT        COUNT('количество заключенных договоров') AS 'количество_заключенных_договоров', ИНН, SUM(TabContract.сумма_заключенных_договоров) as 'сумма_заключенных_договоров'
                               FROM(SELECT        COUNT(dbo.Договор.НомерДоговора) AS 'количество заключенных договоров', dbo.Поликлинника.ИНН, SUM(dbo.УслугиПоДоговору.Сумма) AS 'сумма_заключенных_договоров'
                                                         FROM            dbo.Договор INNER JOIN
                                                                                   dbo.Поликлинника ON dbo.Поликлинника.id_поликлинника = dbo.Договор.id_поликлинника INNER JOIN
                                                                                   dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор
                                                         WHERE(dbo.Договор.ФлагПроверки = 1) AND(dbo.Договор.ДатаЗаписиДоговора >= '" + this.strDateStart + "') AND(dbo.Договор.ДатаЗаписиДоговора <= '" + this.strDateEnd + "') " +
                                                        @" GROUP BY dbo.Поликлинника.ИНН, dbo.Договор.НомерДоговора) AS TabContract
                               GROUP BY ИНН) AS TabContractValid
                               ON TabNameHospital.ИНН = TabContractValid.ИНН
                               LEFT OUTER JOIN
                             (SELECT        COUNT('количество договоров находящихся в деле') AS 'количество_договоров_находящихся_в_деле', ИНН, SUM(сумма_договоров_находящихся_в_деле)
                                                         AS 'сумма_договоров_находящихся_в_деле'
                               FROM(SELECT        COUNT(dbo.АктВыполненныхРабот.id_акт) AS 'количество договоров находящихся в деле', dbo.Поликлинника.ИНН, SUM(dbo.УслугиПоДоговору.Сумма)
                                                                                   AS 'сумма_договоров_находящихся_в_деле'
                                                         FROM            dbo.Договор INNER JOIN
                                                                                   dbo.АктВыполненныхРабот ON dbo.Договор.id_договор = dbo.АктВыполненныхРабот.id_договор INNER JOIN
                                                                                   dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор INNER JOIN
                                                                                   dbo.Поликлинника ON dbo.Поликлинника.id_поликлинника = dbo.Договор.id_поликлинника
                                                         WHERE(dbo.Договор.ФлагПроверки = 1) AND(dbo.Договор.ДатаЗаписиДоговора >= '" + this.strDateStart + "') AND(dbo.Договор.ДатаЗаписиДоговора <= '" + this.strDateEnd + "') " +
                                                        @" GROUP BY dbo.Поликлинника.ИНН, dbo.Договор.НомерДоговора) AS TabAct
                               GROUP BY ИНН) AS TabContractPayment ON TabContractValid.ИНН = TabContractPayment.ИНН
                               LEFT OUTER JOIN
                             (SELECT        COUNT('количество договоров находящихся в деле') AS 'количество_договоров_поступивших_на_оплату', ИНН, SUM(сумма_договоров_находящихся_в_деле)
                                                         AS 'сумма_договоро_поступивщих_на_оплату'
                               FROM(SELECT        COUNT(dbo.АктВыполненныхРабот.id_акт) AS 'количество договоров находящихся в деле', dbo.Поликлинника.ИНН, SUM(dbo.УслугиПоДоговору.Сумма)
                                                                                   AS 'сумма_договоров_находящихся_в_деле'
                                                         FROM            dbo.Договор INNER JOIN
                                                                                   dbo.АктВыполненныхРабот ON dbo.Договор.id_договор = dbo.АктВыполненныхРабот.id_договор INNER JOIN
                                                                                   dbo.УслугиПоДоговору ON dbo.Договор.id_договор = dbo.УслугиПоДоговору.id_договор INNER JOIN
                                                                                   dbo.Поликлинника ON dbo.Поликлинника.id_поликлинника = dbo.Договор.id_поликлинника
                                                         WHERE(dbo.Договор.ФлагПроверки = 1) AND(dbo.Договор.ДатаЗаписиДоговора >= '" + this.strDateStart + "') AND(dbo.Договор.ДатаЗаписиДоговора <= '" + this.strDateEnd + "') " +
                                                        @" GROUP BY dbo.Поликлинника.ИНН, dbo.Договор.НомерДоговора) AS TabAct
                               GROUP BY ИНН) AS TabAct ON TabContractValid.ИНН = TabAct.ИНН ";

            return query;
        }

        //IQueryFactory ICreateSqlQuery.SqlQuery()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
