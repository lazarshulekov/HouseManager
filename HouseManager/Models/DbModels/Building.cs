using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseManager.Models
{

    public class Building
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }

    public class Issue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Accepted { get; set; }
        public Meeting Meeting { get; set; }
    }

    public class Questionnaire
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class QuestionnaireUserComments
    {
        public int Id { get; set; }

        public int QuestionnaireId { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; }

        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }

        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }

    public class QuestionnaireUserVotes
    {
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool Agrees { get; set; }
    }

    public class Expenses
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UsersExpenses
    {
        public int ExpenseId { get; set; }
        public ICollection<Expenses> Expenses { get; set; }

        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }
    }
 }
