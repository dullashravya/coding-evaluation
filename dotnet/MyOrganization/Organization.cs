using System;
using System.Text;
using System.Linq;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        public Position? Hire(Name person, string title)
        {
            if (person == null)
                throw new Exception("Person cannot be null");
            if (string.IsNullOrEmpty(title))
                throw new Exception("Title cannot be null or empty");

            return HireHelper(root, person, title);
        }

        private Position? HireHelper(Position position, Name person, string title)
        {
            if (position.GetTitle() == title && !position.IsFilled())
            {
                Employee newEmployee = new Employee(new Random().Next(), person); // assuming identifier is random
                position.SetEmployee(newEmployee);
                return position;
            }

            foreach (var report in position.GetDirectReports())
            {
                var filledPosition = HireHelper(report, person, title);
                if (filledPosition != null)
                    return filledPosition;
            }

            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
