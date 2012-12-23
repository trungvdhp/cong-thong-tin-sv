using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CongThongTinSV.Controllers;
using CongThongTinSV.Models;

namespace CongThongTinSV.App_Lib
{
    public class GlobalLib
    {
        #region User
        /// <summary>
        /// Get current user data
        /// </summary>
        /// <returns>Current user data</returns>
        public static UserData GetCurrentUserData()
        {
            UserData user = new UserData();
            try
            {
                string[] s = (((FormsIdentity)System.Web.HttpContext.Current.User.Identity).Ticket.UserData.Split('|'));
                user = new UserData
                {
                    UserName = System.Web.HttpContext.Current.User.Identity.Name,
                    PortalUserGroup = Convert.ToInt32(s[0]),
                    PortalUserID = Convert.ToInt32(s[1]),
                    PortalFullName = s[2],
                    MoodleToken = s[3],
                    MoodleService = s[4],
                    MoodleUserID = Convert.ToInt64(s[5]),
                    MoodleFullName = s[6]
                };

            }
            catch (Exception)
            {
            }

            return user;
        }
        #endregion

        #region Controller
        /// <summary>
        /// Get sub classes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<Type> GetSubClasses<T>()
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(
                    a => a.GetTypes().Where(type => type.IsSubclassOf(typeof(T)))
                ).ToList();
        }

        /// <summary>
        /// Get controller names
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetControllerNames()
        {
            return GetSubClasses<Controller>().Select(t => t.Name);
        }
 
        /// <summary>
        /// Get list method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MOD_Quyen> GetControllerActionMethods()
        {
            List<Type> controllers = GetSubClasses<Controller>();
            List<MOD_Quyen> methods = new List<MOD_Quyen>();

            foreach(Type item in controllers)
            {
                MethodInfo[] list = item.GetMethods();
                string controllername = item.Name.Substring(0, item.Name.Length - "Controller".Length);

                foreach (MethodInfo m in list)
                {
                    if (m.IsPublic)
                    {
                        if (typeof(ActionResult).IsAssignableFrom(m.ReturnParameter.ParameterType) ||
                            typeof(JsonResult).IsAssignableFrom(m.ReturnParameter.ParameterType))
                        {
                            AuthorizeAttribute authorize = (AuthorizeAttribute)m.GetCustomAttribute(typeof(AuthorizeAttribute), false);
                            AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)m.GetCustomAttribute(typeof(AssemblyDescriptionAttribute));

                            string action_name = controllername + "." + m.Name;

                            if (authorize != null && authorize.Roles == action_name && !methods.Any(a => a.Action_name == action_name))
                            {
                                methods.Add(new MOD_Quyen{
                                    Ten_quyen = description == null ? action_name : description.Description,
                                    Action_name = action_name
                                });
                            }
                        }
                    }
                }
            }

            return methods;
        }
        #endregion

        #region Capability
        /// <summary>
        /// Get capabilities
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Capability> GetCapabilities()
        {
            Entities db = new Entities();

            return (from quyen in db.MOD_Quyen
                   select new Capability
                   {
                       ID_quyen = quyen.ID_quyen,
                       Ten_quyen = quyen.Ten_quyen,
                       Action_name = quyen.Action_name
                   }).AsEnumerable().OrderBy(t => t.Action_name.Split(new char[]{'.'})[0]);
        }

        /// <summary>
        /// Get list of actions by service shortname
        /// </summary>
        /// <param name="serviceShortName">Shortname of service</param>
        /// <returns></returns>
        public static string[] GetActionNamesByService(string serviceShortName)
        {
            Entities db = new Entities();
            MOD_DichVu service = db.MOD_DichVu.SingleOrDefault(t => t.Ten_rut_gon == serviceShortName);

            if (service != null)
            {
                if (service.Root == true)
                {
                    List<string> actions = db.MOD_Quyen.Select(t => t.Action_name).ToList();
                    actions.Add("Admin");
                    actions.Add("MoodleAdmin");
                    return actions.ToArray();
                }
                else
                {
                    return service.MOD_Quyen.Select(t => t.Action_name).ToArray();
                }
            }

            return new string[] { };
        }

        /// <summary>
        /// Create capability
        /// </summary>
        /// <param name="capability">Capability</param>
        /// <returns></returns>
        public static int CreateCapability(Capability capability)
        {
            Entities db = new Entities();

            db.MOD_Quyen.Add(new MOD_Quyen
            {
                Ten_quyen = capability.Ten_quyen,
                Action_name = capability.Action_name
            });

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Delete capability
        /// </summary>
        /// <param name="capability">Capability</param>
        /// <returns></returns>
        public static int DeleteCapability(Capability capability)
        {
            Entities db = new Entities();
            MOD_Quyen entity = db.MOD_Quyen.SingleOrDefault(t => t.ID_quyen == capability.ID_quyen);

            if (entity != null)
            {
                db.MOD_Quyen.Remove(entity);
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Delete list of capabilities
        /// </summary>
        /// <param name="ids">list of capabilities ids</param>
        /// <returns></returns>
        public static int DeleteCapabilities(IEnumerable<string> ids)
        {
            Entities db = new Entities();

            foreach (string id in ids)
            {
                MOD_Quyen entity = db.MOD_Quyen.AsEnumerable().SingleOrDefault(t => t.ID_quyen.ToString() == id);

                if (entity != null)
                {
                    db.MOD_Quyen.Remove(entity);
                }
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Update capability
        /// </summary>
        /// <param name="capability">Capability</param>
        /// <returns></returns>
        public static int UpdateCapability(Capability capability)
        {
            Entities db = new Entities();
            MOD_Quyen entity = db.MOD_Quyen.SingleOrDefault(t => t.ID_quyen == capability.ID_quyen);

            if (entity != null)
            {
                entity.Ten_quyen = capability.Ten_quyen;
                entity.Action_name = capability.Action_name;
                db.Entry(entity).State = System.Data.EntityState.Modified;
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }

        /// <summary>
        /// Synchronize capability
        /// </summary>
        /// <returns></returns>
        public static int SyncCapability()
        {
            Entities db = new Entities();
            var methods = GetControllerActionMethods();
            var methodnames = methods.Select(t => t.Action_name);
            var quyens = db.MOD_Quyen.AsQueryable().Where(t => !methodnames.Contains(t.Action_name));

            foreach (MOD_Quyen quyen in quyens)
            {
                db.MOD_Quyen.Remove(quyen);
            }

            var actionnames = db.MOD_Quyen.Select(t => t.Action_name);
            var actions = methods.Where(t => !actionnames.Contains(t.Action_name));

            foreach (MOD_Quyen action in actions)
            {
                db.MOD_Quyen.Add(new MOD_Quyen
                {
                    Ten_quyen = action.Ten_quyen,
                    Action_name = action.Action_name
                });
            }

            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
            }

            return -1;
        }
        #endregion
    }
}