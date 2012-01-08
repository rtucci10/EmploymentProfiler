
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace RS.EmploymentProfiler.Repository
{
    public interface IRepository<T> where T : class
    {    
		#region	Methods
	
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);        
        T Add(T entity);
        void Remove(T entity);   
		
		#endregion
    }
	
	public abstract class Repository<T> : IRepository<T>
                                  where T : class
    {
    	#region Members

    	protected IObjectSet<T> _objectSet;
		private ObjectContext _context;

    	#endregion

    	#region Ctor

    	public Repository(ObjectContext context)
    	{
      		_context = context;
			_objectSet = context.CreateObjectSet<T>();			
    	}
		
		public Repository() 
        {
            _context = new EmploymentProfilerDB();
            _objectSet = _context.CreateObjectSet<T>();
        }

    	#endregion

    	#region IRepository<T> Members

    	public IEnumerable<T> GetAll()
    	{
      		return _objectSet;
    	}

    	public abstract T GetById(int id);

    	public IEnumerable<T> Query(Expression<Func<T, bool>> filter)
    	{
      		return _objectSet.Where(filter);
    	}

    	public T Add(T entity)
    	{
     	 	_objectSet.AddObject(entity);
            _context.SaveChanges();
            return entity;			
    	}

    	public void Remove(T entity)
    	{
      		_objectSet.DeleteObject(entity);
    	}

    	#endregion
  	}

	
	public partial class AccomplishmentRepository : Repository<Accomplishment>
	{
		#region Ctor

		public AccomplishmentRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public AccomplishmentRepository() { }

		#endregion

		#region Methods
 
		public override Accomplishment GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class AddressRepository : Repository<Address>
	{
		#region Ctor

		public AddressRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public AddressRepository() { }

		#endregion

		#region Methods
 
		public override Address GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class CompanyRepository : Repository<Company>
	{
		#region Ctor

		public CompanyRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public CompanyRepository() { }

		#endregion

		#region Methods
 
		public override Company GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class PersonRepository : Repository<Person>
	{
		#region Ctor

		public PersonRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public PersonRepository() { }

		#endregion

		#region Methods
 
		public override Person GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class PersonCompanyRepository : Repository<PersonCompany>
	{
		#region Ctor

		public PersonCompanyRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public PersonCompanyRepository() { }

		#endregion

		#region Methods
 
		public override PersonCompany GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.People_Id == id);
		}

		#endregion		
	}
	
	public partial class PortfolioItemRepository : Repository<PortfolioItem>
	{
		#region Ctor

		public PortfolioItemRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public PortfolioItemRepository() { }

		#endregion

		#region Methods
 
		public override PortfolioItem GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class PortfolioItemTypeRepository : Repository<PortfolioItemType>
	{
		#region Ctor

		public PortfolioItemTypeRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public PortfolioItemTypeRepository() { }

		#endregion

		#region Methods
 
		public override PortfolioItemType GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class SkillRepository : Repository<Skill>
	{
		#region Ctor

		public SkillRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public SkillRepository() { }

		#endregion

		#region Methods
 
		public override Skill GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
	
	public partial class SkillTypeRepository : Repository<SkillType>
	{
		#region Ctor

		public SkillTypeRepository(ObjectContext context)
   			: base(context)
		{
		}
		
		public SkillTypeRepository() { }

		#endregion

		#region Methods
 
		public override SkillType GetById(int id)   
		{
			return _objectSet.SingleOrDefault(e => e.Id == id);
		}

		#endregion		
	}
		
  public interface IUnitOfWork
  {
  	#region	Methods
	
			IRepository<Accomplishment> Accomplishments { get; }   
			IRepository<Address> Addresses { get; }   
			IRepository<Company> Companies { get; }   
			IRepository<Person> People { get; }   
			IRepository<PersonCompany> PersonCompanies { get; }   
			IRepository<PortfolioItem> PortfolioItems { get; }   
			IRepository<PortfolioItemType> PortfolioItemTypes { get; }   
			IRepository<Skill> Skills { get; }   
			IRepository<SkillType> SkillTypes { get; }   
	    void Commit();
	
	#endregion
  }

  public partial class UnitOfWork : IUnitOfWork
  {
    #region Members

    private readonly ObjectContext _context;
		private AccomplishmentRepository _accomplishments;
		private AddressRepository _addresses;
		private CompanyRepository _companies;
		private PersonRepository _people;
		private PersonCompanyRepository _personcompanies;
		private PortfolioItemRepository _portfolioitems;
		private PortfolioItemTypeRepository _portfolioitemtypes;
		private SkillRepository _skills;
		private SkillTypeRepository _skilltypes;
	    
    #endregion

    #region Ctor

    public UnitOfWork(ObjectContext context)
    {
      if (context == null)
      {
        throw new ArgumentNullException("context wasn't supplied");
      }

      _context = context;
    }

    #endregion

    #region IUnitOfWork Members

		public IRepository<Accomplishment> Accomplishments
	{
		get
		{
			if (_accomplishments == null)
			{
				_accomplishments = new AccomplishmentRepository(_context);
			}
			return _accomplishments;
		}
	}
		public IRepository<Address> Addresses
	{
		get
		{
			if (_addresses == null)
			{
				_addresses = new AddressRepository(_context);
			}
			return _addresses;
		}
	}
		public IRepository<Company> Companies
	{
		get
		{
			if (_companies == null)
			{
				_companies = new CompanyRepository(_context);
			}
			return _companies;
		}
	}
		public IRepository<Person> People
	{
		get
		{
			if (_people == null)
			{
				_people = new PersonRepository(_context);
			}
			return _people;
		}
	}
		public IRepository<PersonCompany> PersonCompanies
	{
		get
		{
			if (_personcompanies == null)
			{
				_personcompanies = new PersonCompanyRepository(_context);
			}
			return _personcompanies;
		}
	}
		public IRepository<PortfolioItem> PortfolioItems
	{
		get
		{
			if (_portfolioitems == null)
			{
				_portfolioitems = new PortfolioItemRepository(_context);
			}
			return _portfolioitems;
		}
	}
		public IRepository<PortfolioItemType> PortfolioItemTypes
	{
		get
		{
			if (_portfolioitemtypes == null)
			{
				_portfolioitemtypes = new PortfolioItemTypeRepository(_context);
			}
			return _portfolioitemtypes;
		}
	}
		public IRepository<Skill> Skills
	{
		get
		{
			if (_skills == null)
			{
				_skills = new SkillRepository(_context);
			}
			return _skills;
		}
	}
		public IRepository<SkillType> SkillTypes
	{
		get
		{
			if (_skilltypes == null)
			{
				_skilltypes = new SkillTypeRepository(_context);
			}
			return _skilltypes;
		}
	}
	    
	
    public void Commit()
    {
      _context.SaveChanges();
    }

    #endregion
  }
}
