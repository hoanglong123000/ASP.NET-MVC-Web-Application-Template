﻿using System;
using System.Collections.Generic;
using System.Linq; 
using Service.Utility.Variables;
using Service.Education.Executes.Clothesmn.Clothes;
using Service.Education.Executes.Clothesmn.SoldCoupons;


namespace Service.Education.Executes.Base
{
    public partial class EducationService
    {
        public SoldCouponViewModel SoldCouponOne(int id)
        {
            CheckDbConnect();
            var item = Context.Database.SqlQuery<SoldCouponViewModel>("SELECT TOP 1 * from SoldCoupons as S WHERE S.Id = " + id).FirstOrDefault();
            if(item != null)
            {
                var ids = new List<Guid>();
                ids.Add(item.UpdatedBy);
                ids.Add(item.CreatedBy);

                var buyernameid = new List<int>();
                buyernameid.Add(item.BuyerName);

                ids = ids.Distinct().ToList();
                buyernameid = buyernameid.Distinct().ToList();

                var emps = _shareService.EmployeeBaseList(new Core.Executes.Employees.Employees.SearchEmployeeModel
                {
                    Ids = ids
                });

                var buyername = NameList(new SearchSoldCouponModel
                {
                    Ids = buyernameid
                });

                item.ObjUpdatedBy = emps.FirstOrDefault(x => x.Id == item.UpdatedBy);
                item.ObjCreatedBy = emps.FirstOrDefault(x => x.Id == item.CreatedBy);
                item.ObjNameBuyer = buyername.FirstOrDefault(x => x.Id == item.BuyerName);
            }
            return item;

            
        }
    }
}