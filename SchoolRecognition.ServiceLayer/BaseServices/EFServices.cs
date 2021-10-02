using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolRecognition.DBInfrastructure.DbContexts;
using SchoolRecognition.ServiceLayer.Extensions;
using SchoolRecognition.ServiceLayer.HelperServices;
using SchoolRecognition.ServiceLayer.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRecognition.ServiceLayer.BaseServices
{
    public class EFServices<Dto, DBTable> : IEFServices<Dto, DBTable> where Dto : class where DBTable : class
    {
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        protected WSchoolRecognitionContext DbContext
        {
            get;
            set;
        }
        protected DbSet<DBTable> DbSet
        {
            get;
            set;
        }

        public EFServices(WSchoolRecognitionContext context, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            this.DbSet = this.DbContext.Set<DBTable>();
        }

        public virtual IQueryable<DBTable> GetQueryable()
        {
            return this.DbSet;
        }
        public async Task<IEnumerable<Dto>> ListAsNoTrackingAsync()
        {
            try
            {
                List<Dto> returnValue = new List<Dto>();

                var dbResult = await this.DbSet.AsNoTracking().ToListAsync();

                if (dbResult != null)
                {
                    returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        public async Task<IEnumerable<Dto>> ListAsync()
        {
            try
            {
                List<Dto> returnValue = new List<Dto>();

                var dbResult = await this.DbSet.ToListAsync();

                if (dbResult != null)
                {
                    returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedList<Dto>> PagedListAsNoTrackingAsync(BaseResourceParams<Dto> resourceParams, List<string> searchFields)
        {
            try
            {
                //Instantiate Return Value
                PagedList<Dto> returnValue = PagedList<Dto>
                            .Create(Enumerable.Empty<Dto>().AsQueryable(),
                                resourceParams.PageNumber,
                                resourceParams.PageSize);

                if (resourceParams != null)
                {

                    IQueryable<DBTable> dbResult = this.DbSet;
                    //Search
                    if (!string.IsNullOrWhiteSpace(resourceParams.SearchQuery))
                    {

                        dbResult = dbResult.ResolveSearchParams(resourceParams.SearchQuery, resourceParams.SearchFields);
                    }
                    //Ordering
                    if (!string.IsNullOrWhiteSpace(resourceParams.OrderBy))
                    {
                        // get property mapping dictionary
                        var compositeSubjectPropertyMappingDictionary =
                            _propertyMappingService.GetPropertyMapping<Dto, DBTable>();

                        dbResult = dbResult.ApplySort(resourceParams.OrderBy,
                            compositeSubjectPropertyMappingDictionary);
                    }
                    //Filter if any                    
                    dbResult = dbResult.ApplyFilter(resourceParams.FilterParameters);



                    var pagedDbResult = await PagedList<DBTable>.CreateAsync(dbResult,
                        resourceParams.PageNumber,
                        resourceParams.PageSize);

                    List<Dto> mappedViewDto = _mapper.Map<List<Dto>>(pagedDbResult.ToList());

                    returnValue = new PagedList<Dto>(mappedViewDto, pagedDbResult.TotalCount, resourceParams.PageNumber, resourceParams.PageSize);

                    return returnValue;
                }
                else
                {
                    throw new ArgumentNullException(nameof(resourceParams));
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        
        //public Task<PagedList<Dto>> List(BaseResourceParams<Dto> resourceParams, DynamicParameters parms)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<Dto> GetAsync(object primaryKey, string filterField)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //
                Dto returnValue = null;

                if (primaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                {
                    //var dbResult = await this.DbSet.ToListAsync();

                    //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                    for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                    {
                        var objectProperty = typeof(DBTable).GetProperties()[i];

                        if (objectProperty.Name == filterField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_propertyName))
                    {
                        return returnValue;
                    }

                    //covert the filterField to the same type as the property
                    type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                    if (type == typeof(Guid?))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else if (type == typeof(Guid))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else
                    {
                        searchValue = Convert.ChangeType(primaryKey.ToString(), type);
                    }



                    var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                    IQueryable<DBTable> collection = this.DbSet;
                    collection = collection.Where(predicate).Cast<DBTable>();

                    var dbResult = await collection.FirstOrDefaultAsync();
                    if (dbResult != null)
                    {
                        returnValue = _mapper.Map<Dto>(dbResult);
                    }

                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Dto> GetAsNoTrackingAsync(object primaryKey, string filterField)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //
                Dto returnValue = null;

                if (primaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                {
                    //var dbResult = await this.DbSet.ToListAsync();

                    //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                    for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                    {
                        var objectProperty = typeof(DBTable).GetProperties()[i];

                        if (objectProperty.Name == filterField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_propertyName))
                    {
                        return returnValue;
                    }

                    //covert the filterField to the same type as the property
                    type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                    if (type == typeof(Guid?))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else if (type == typeof(Guid))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else
                    {
                        searchValue = Convert.ChangeType(primaryKey.ToString(), type);
                    }



                    var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                    IQueryable<DBTable> collection = this.DbSet;
                    collection = collection.AsNoTracking().Where(predicate).Cast<DBTable>();

                    var dbResult = await collection.FirstOrDefaultAsync();
                    if (dbResult != null)
                    {
                        returnValue = _mapper.Map<Dto>(dbResult);
                    }

                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CreateAsync(DBTable _obj)
        {
            try
            {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                //map the expected data to the entity object
                var entity = _obj;

                EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                if (dbEntityEntry.State != EntityState.Detached) //1
                {
                    dbEntityEntry.State = EntityState.Added; //4
                    return true;
                }
                await this.DbSet.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public async Task<bool> CreateAsync(Dto _obj)
        {
            try
            {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                //map the expected data to the entity object
                var entity = _mapper.Map<DBTable>(_obj);

                EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                if (dbEntityEntry.State != EntityState.Detached) //1
                {
                    dbEntityEntry.State = EntityState.Added; //4
                    return true;
                }
                await this.DbSet.AddAsync(entity);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CreateMultipleAsync(List<Dto> _obj)
        {
            try
            {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                List<DBTable> list = new List<DBTable>();
                //map the expected data to the entity object
                var entitys = _mapper.Map<List<DBTable>>(_obj);
                //
                for (int i = 0; i < entitys.Count(); i++)
                {
                    var entity = entitys[i];
                    EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                    if (dbEntityEntry.State != EntityState.Detached) //1
                    {
                        dbEntityEntry.State = EntityState.Added; //4
                        return true;
                    }

                    list.Add(entity);

                }

                await this.DbSet.AddRangeAsync(list);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        public async Task<bool> CreateMultipleAsync(List<DBTable> _obj)
        {
            try
            {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                List<DBTable> list = new List<DBTable>();
                for (int i = 0; i < _obj.Count(); i++)
                {
                    //map the expected data to the entity object
                    var entity = _obj[i];

                    EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                    if (dbEntityEntry.State != EntityState.Detached) //1
                    {
                        dbEntityEntry.State = EntityState.Added; //4
                        return true;
                    }
                    list.Add(entity);
                }

                await this.DbSet.AddRangeAsync(list);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(Dto _obj)
        {
            try
            {
                if (_obj == null)
                {
                    throw new ArgumentNullException(nameof(_obj));
                }
                //map the expected data to the entity object
                var entity = _mapper.Map<DBTable>(_obj);

                EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                if (dbEntityEntry.State == EntityState.Detached) //1
                {
                    this.DbSet.Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified; //16

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Remove(object primaryKey, string filterField)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //

                if (primaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                {
                    //var dbResult = await this.DbSet.ToListAsync();

                    //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                    for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                    {
                        var objectProperty = typeof(DBTable).GetProperties()[i];

                        if (objectProperty.Name == filterField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_propertyName))
                    {
                        return true;
                    }

                    //covert the filterField to the same type as the property
                    type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                    if (type == typeof(Guid?))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else if (type == typeof(Guid))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else
                    {
                        searchValue = Convert.ChangeType(primaryKey.ToString(), type);
                    }



                    var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                    IQueryable<DBTable> collection = this.DbSet;
                    collection = collection.Where(predicate).Cast<DBTable>();

                    var entity = await collection.FirstOrDefaultAsync();
                    if (entity != null)
                    {
                        EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                        if (dbEntityEntry.State != EntityState.Deleted) //8
                        {
                            dbEntityEntry.State = EntityState.Deleted; //8
                            return true;
                        }
                        this.DbSet.Attach(entity);
                        this.DbSet.Remove(entity);
                    }

                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        

        public async Task<bool> Remove(DBTable _obj)
        {
            try
            {
                if (_obj != null)
                {
                    var entity = _obj;

                    EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                    if (dbEntityEntry.State != EntityState.Deleted) //8
                    {
                        dbEntityEntry.State = EntityState.Deleted; //8
                        return true;
                    }
                    this.DbSet.Attach(entity);
                    this.DbSet.Remove(entity);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }        

        public async Task<bool> Remove(List<DBTable> _objs)
        {
            try
            {
                if (_objs != null)
                {
                    List<DBTable> list = new List<DBTable>();

                    for (int i = 0; i < _objs.Count(); i++)
                    {
                        var entity = _objs[i];

                        EntityEntry dbEntityEntry = this.DbContext.Entry<DBTable>(entity);
                        if (dbEntityEntry.State != EntityState.Deleted) //8
                        {
                            dbEntityEntry.State = EntityState.Deleted; //8
                            return true;
                        }

                        list.Add(entity);
                    }
                    this.DbSet.AttachRange(list);
                    this.DbSet.RemoveRange(list);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(object primaryKey, string filterField)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //

                if (primaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                {
                    //var dbResult = await this.DbSet.ToListAsync();

                    //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                    for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                    {
                        var objectProperty = typeof(DBTable).GetProperties()[i];

                        if (objectProperty.Name == filterField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_propertyName))
                    {
                        return false;
                    }

                    //covert the filterField to the same type as the property
                    type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                    if (type == typeof(Guid?))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else if (type == typeof(Guid))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else
                    {
                        searchValue = Convert.ChangeType(primaryKey.ToString(), type);
                    }



                    var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                    IQueryable<DBTable> collection = this.DbSet;

                    //
                    return await collection.AnyAsync(predicate);

                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ExistsAsNoTrackingAsync(object primaryKey, string filterField)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //

                if (primaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                {
                    //var dbResult = await this.DbSet.ToListAsync();

                    //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                    for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                    {
                        var objectProperty = typeof(DBTable).GetProperties()[i];

                        if (objectProperty.Name == filterField)
                        {
                            _propertyName = objectProperty.Name;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(_propertyName))
                    {
                        return false;
                    }

                    //covert the filterField to the same type as the property
                    type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                    if (type == typeof(Guid?))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else if (type == typeof(Guid))
                    {
                        searchValue = new Guid(primaryKey.ToString());
                    }
                    else
                    {
                        searchValue = Convert.ChangeType(primaryKey.ToString(), type);
                    }



                    var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                    IQueryable<DBTable> collection = this.DbSet;

                    //
                    return await collection.AnyAsync(predicate);

                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(BaseResourceParams<Dto> resourceParameters, string filterField, bool shouldOnlyCheckEntitiesWithDifferentId)
        {
            try
            {
                string _propertyName = null;
                object searchValue;
                Type type;
                //instantiate return value
                bool returnValue = false;

                if (resourceParameters != null && resourceParameters.FilterParameters.Count() > 0)
                {


                    if (resourceParameters.PrimaryKey != null && !string.IsNullOrWhiteSpace(filterField))
                    {
                        //var dbResult = await this.DbSet.ToListAsync();

                        //returnValue = _mapper.Map<IEnumerable<Dto>>(dbResult).ToList();
                        for (int i = 0; i < typeof(DBTable).GetProperties().Length; i++)
                        {
                            var objectProperty = typeof(DBTable).GetProperties()[i];

                            if (objectProperty.Name == filterField)
                            {
                                _propertyName = objectProperty.Name;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(_propertyName))
                        {
                            return returnValue;
                        }

                        //covert the filterField to the same type as the property
                        type = typeof(DBTable).GetProperty(_propertyName).PropertyType;
                        if (type == typeof(Guid?))
                        {
                            searchValue = new Guid(resourceParameters.PrimaryKey.ToString());
                        }
                        else if (type == typeof(Guid))
                        {
                            searchValue = new Guid(resourceParameters.PrimaryKey.ToString());
                        }
                        else
                        {
                            searchValue = Convert.ChangeType(resourceParameters.PrimaryKey.ToString(), type);
                        }



                        var predicate = ExpressionBuilder.BuildPredicate<DBTable>(searchValue, OperatorComparer.Equals, _propertyName);
                        IQueryable<DBTable> collection = this.DbSet;
                        collection = collection.Where(predicate).Cast<DBTable>();

                        collection = collection.ApplyFilter(resourceParameters.FilterParameters);

                        returnValue = await collection.AnyAsync();

                    }
                }

                return returnValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await Task.Run(async () =>
            {
                return (await DbContext.SaveChangesAsync() >= 0);
            });

        }

        public bool Save()
        {
            return DbContext.SaveChanges() >= 0;

        }
    }
}
