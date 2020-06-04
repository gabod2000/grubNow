using Microsoft.Extensions.DependencyInjection;
using Models;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class BusinessBase<T> : IBusinessBase<T> where T : class
    {
        protected RepositoryBase<T> repositoryBase;
        protected GrubNowDbContext plenumDbContext;
        protected IServiceProvider _serviceProvider;
        public BusinessBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            plenumDbContext = serviceProvider.GetRequiredService<GrubNowDbContext>();
            repositoryBase = new RepositoryBase<T>(serviceProvider);
        }
        public virtual async Task<int> Post(T entity, bool saveChanges = true)
        {
            //setCreateAnalysisFields(entity);
            int result = await repositoryBase.Post(entity, saveChanges);
            return result;
        }

        public virtual async Task<int> Delete(int id, bool saveChanges = true)
        {
            try
            {
                int result = await repositoryBase.Delete(id, saveChanges);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public virtual async Task<int> DeleteRange(IEnumerable<int> entityIDs, bool saveChanges = true)
        {
            return await repositoryBase.DeleteRange(entityIDs, saveChanges);
        }

        public virtual IEnumerable<T> Get()
        {
            return repositoryBase.Get().AsQueryable();
        }

        public virtual T GetById(int id)
        {
            return repositoryBase.GetById(id);
        }

        public virtual IEnumerable<T> GetWithCondition(Expression<Func<T, bool>> expression)
        {
            return repositoryBase.GetWithCondition(expression);
        }

        public virtual async Task<int> Put(int id, T entity, bool saveChanges = true)
        {
           // setUpdateAnalysisFields(entity);
            int result = await repositoryBase.Put(entity, saveChanges);
            return result;
        }

        public void setUpdateAnalysisFields(T entity)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "modifiedby");

            property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "modifieddate");
            if (property != null)
                property.SetValue(entity, DateTime.Now);
        }
        public void setCreateAnalysisFields(T entity)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "createdby");
            property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "createddate");
            if (property != null)
                property.SetValue(entity, DateTime.Now);
        }
    }
}
