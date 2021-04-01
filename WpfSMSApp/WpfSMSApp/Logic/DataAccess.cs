using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSMSApp.Model;

namespace WpfSMSApp.Logic
{
    public class DataAccess
    {
        /// <summary>
        /// 입력과 수정을 동시에 수행
        /// </summary>
        /// <param name="user"></param>
        /// <returns>0또는 1이상</returns>
        internal static int SetUser (User user)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.User.AddOrUpdate(user);
                return ctx.SaveChanges();//commit
            }
        }

        public static List<User> GetUsers()
        {
            List<User> users;

            using(var ctx = new SMSEntities())
            {
                users = ctx.User.ToList();//=SELECT * FROM user
            }

            return users;
        }

        public static List<Stock> GetStocks()
        {
            List<Stock> stocks;

            using (var ctx = new SMSEntities())
            {
                stocks = ctx.Stock.ToList();//=SELECT * FROM user
            }

            return stocks;
        }

        public static List<Store> GetStores()
        {
            List<Store> stores;

            using (var ctx = new SMSEntities())
            {
                stores = ctx.Store.ToList();//=SELECT * FROM user
            }

            return stores;
        }

        public static int SetStore(Store store)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.Store.AddOrUpdate(store);
                return ctx.SaveChanges();
            }
        }
    }
}
