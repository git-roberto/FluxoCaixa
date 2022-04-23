using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilidades.EntityHelpers;

namespace Utilidades.Base
{
    public class ServiceBase<T> : IDisposable where T : class
    {

        public ContextoBase Conexao { get; set; }

        public ServiceBase(ContextoBase _context)
        {
            //Abre a conexão com o BD
            Conexao = _context;
        }

        /// <summary>
        /// Lista todos os registros
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Listar()
        {
            var lst = Conexao.Set<T>().AsQueryable();

            return lst;
        }

        /// <summary>
        /// Lista os registros de acordo com o filtro (LAMBDA)
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Listar(Expression<Func<T, bool>> filtro)
        {
            var lst = Conexao.Set<T>().Where(filtro).AsQueryable();

            return lst;
        }

        /// <summary>
        /// Busca um registro pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Buscar(int id)
        {
            var registro = Conexao.Set<T>().Find(id);

            return registro;
        }

        /// <summary>
        /// Busca um registro pelo filtro
        /// </summary>
        /// <returns></returns>
        public virtual T Buscar(Expression<Func<T, bool>> filtro)
        {
            var registro = Conexao.Set<T>().FirstOrDefault(filtro);

            return registro;
        }

        /// <summary>
        /// Insere um registro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public virtual T Inserir(T registro)
        {

            var retorno = Conexao.Set<T>().Add(registro);
            Conexao.SaveChanges();

            return retorno;
        }

        /// <summary>
        /// ALtera um registro
        /// </summary>
        /// <param name="registro"></param>
        public virtual void Alterar(T registro)
        {

            Conexao.Entry(registro).State = EntityState.Modified;
            Conexao.SaveChanges();
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="registro"></param>
        public virtual void Excluir(T registro)
        {
            Conexao.Set<T>().Remove(registro);
            Conexao.SaveChanges();
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="id"></param>
        public virtual void Excluir(int id)
        {
            var registro = Buscar(id);

            Excluir(registro);
        }

        /// <summary>
        /// Insere um registro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public virtual T Inserir(T registro, int idUsuario)
        {

            var retorno = Conexao.Set<T>().Add(registro);
            Conexao.SaveChanges();

            return retorno;
        }

        /// <summary>
        /// ALtera um registro
        /// </summary>
        /// <param name="registro"></param>
        public virtual void Alterar(T registro, int idUsuario)
        {

            Conexao.Entry(registro).State = EntityState.Modified;
            Conexao.SaveChanges();
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="registro"></param>
        public virtual void Excluir(T registro, int idUsuario)
        {
            Conexao.Set<T>().Remove(registro);
            Conexao.SaveChanges();
        }

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="id"></param>
        public virtual void Excluir(int id, int idUsuario)
        {
            var registro = Buscar(id);

            Excluir(registro, idUsuario);
        }

        public DbContextTransaction BeginTransaction()
        {
            var retorno = Conexao.Database.BeginTransaction();

            return retorno;
        }

        public void Commit(DbContextTransaction transaction)
        {
            transaction.Commit();
        }

        public void RollBack(DbContextTransaction transaction)
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            //Fecha a conexão com o BD
            if (Conexao != null)
            {
                Conexao.Dispose();
                Conexao = null;
            }
        }
    }
}
