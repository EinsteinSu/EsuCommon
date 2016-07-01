using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IQuestionManager : ICrud<Question>, IDisposable
    {
        string CreateAnswer(Question question, Answer answer);
    }

    public class QuestionManager : IQuestionManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<Question> SelectList()
        {
            return _context.Questions.ToList();
        }

        public Question SelectById(int id)
        {
            return _context.Questions.FirstOrDefault(f => f.QuestionId == id);
        }

        public string Create(Question question)
        {
            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(Question question)
        {
            try
            {
                var original = SelectById(question.QuestionId);
                if (original == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                _context.Entry(original).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var original = SelectById(id);
                if (original == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                _context.Questions.Remove(original);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string CreateAnswer(Question question, Answer answer)
        {
            try
            {
                question.Answers.Add(answer);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public static class CrudExtentions
    {
        public static string Create<T>(T data, IDbSet<T> collection, ExaDataContext context) where T : class
        {
            try
            {
                collection.Add(data);
                context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}