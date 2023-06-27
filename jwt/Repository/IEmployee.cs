using jwt.Data;
using jwt.Model;
using Microsoft.EntityFrameworkCore;

namespace jwt.Repository
{
    public interface IEmployee
    {
        public List<employee> list(); 
        public employee GetEmployee(int id);
        public bool create(employee employee);
        public bool update(employee emp,int id);
        public bool remove(int id);
    }

    public class implementIEmployee:IEmployee
    {
        private readonly AppDbContext _context;

 
        public implementIEmployee(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public List<employee> list ()
        {  
             var list = _context.emp.ToList();
            if(list.Count == 0)
            {
                return new List<employee>();
            }
             return list;
         
        }

        public bool create(employee employee)
        {
            
            if(employee == null)
            {
                return false;
            }

            _context.emp.Add(employee);
             int x = _context.SaveChanges();

            return (x > 0) ? true : false;
        }

        public employee GetEmployee(int id)
        {
           if(id <= 0)
            {
                return null;
            }
           if(_context.emp.Find(id) == null)
            {
                return null;
            }

            return _context.emp.Find(id);
        }

        public bool remove(int id )
        {
           if(id <=0)
            {
                return false;
            }
            var emp =_context.emp.Find(id);
            if(emp == null)
            {
                return false;
            }
            _context.emp.Remove(emp);
            int x=_context.SaveChanges();

            return (x>0)? true: false; 
            

        }

        public bool update(employee emp, int id)
        {
          if(emp == null || id <=0) 
            {
                return false;
            }
           
            if (emp.id !=id )
            { 
                return false;
            }
            var e = _context.emp.Find(emp.id);
     
            if(e == null)
            {
                return false;
            }

            //is like Mapping
            _context.Entry(e).CurrentValues.SetValues(emp);
            _context.Entry(e).State = EntityState.Modified;

            _context.emp.Update(e);
           int x= _context.SaveChanges();

           return (x > 0) ? true : false;




        }
    }
}
