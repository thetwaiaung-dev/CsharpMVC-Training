using System.Data;

namespace MvcTraining.Exceptions
{
    public class DuplicateName : DuplicateNameException
    {
        public DuplicateName(string message) :base(message)
        {
          
        }
    }
}
