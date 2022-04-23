using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades.EntityHelpers;

namespace Utilidades.DTO
{
    public abstract class BaseDTO<T, K> : InnerBaseDTO<T> where T : class where K : InnerBaseDTO<T>
    {
        public abstract T Modelo(ContextoBase conexao);
    }

    public abstract class InnerBaseDTO<T> where T : class
    {
    }

    public static class ModelConverter
    {
        public static K ToDTO<T, K>(this T registro) where K : BaseDTO<T, K> where T : class
        {
            try
            {
                if (registro == null)
                {
                    return null;
                }
                var c = typeof(K).GetConstructor(new Type[] { typeof(T) });
                K retorno = c.Invoke(new object[] { registro }) as K;

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T ToEntity<T, K>(this K registro, ContextoBase conexao) where K : BaseDTO<T, K> where T : class
        {
            try
            {
                if (registro == null)
                {
                    return null;
                }
                T retorno = registro.Modelo(conexao);
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
