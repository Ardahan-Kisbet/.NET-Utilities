namespace CSharpUtility.Helpers
{
    public class EnumHelper
    {
        /// <summary>
        /// Extension method for Generic Enum Conversion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string param, bool ignoreCase = true)
        {
            return (T)System.Enum.Parse(typeof(T), param, ignoreCase);
        }
    }
}
