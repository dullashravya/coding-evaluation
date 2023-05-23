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
//         In the Hire method, we first check whether the person or the title is null or empty. If they are, we throw an exception as we cannot hire a person without a name or for a non-existing position.

// Next, we use a helper method, HireHelper, to recursively navigate through the positions. This helper method takes a position, a person and a title. It first checks whether the title of the current position matches the given title and whether the position is already filled. If the title matches and the position is not filled, it creates a new employee and sets this employee for the current position.

// If the title does not match or the position is already filled, the method recursively calls itself for each of the direct reports of the current position.

// If a suitable position is found in one of the direct reports, it is returned. If no suitable position is found in the entire organization, the method returns null.
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
