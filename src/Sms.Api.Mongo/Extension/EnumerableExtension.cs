using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Mongo.Extension
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// 数组添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> Push<T>(this IEnumerable<T> list, T t)
        {
            return null;
        }

        /// <summary>
        /// 数组删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> Pull<T>(this IEnumerable<T> list, T t)
        {
            return null;
        }

        /// <summary>
        /// 数组添加元素（非重复）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> AddToSet<T>(this IEnumerable<T> list, T t)
        {
            return null;
        }
    }
}
