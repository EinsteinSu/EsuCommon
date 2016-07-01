using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IExaminationManager : ICrud<Model.Examination>, IDisposable
    {
        string GenerateExamination(Model.Examination examination, IList<Question> questions = null);
    }

    public class ExaminationManager : IExaminationManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<Model.Examination> SelectList()
        {
            //todo: statistics the score and question count
            return _context.Examinations.ToList();
        }

        public Model.Examination SelectById(int id)
        {
            return _context.Examinations.FirstOrDefault(f => f.ExaminationId == id);
        }

        public string Create(Model.Examination examination)
        {
            try
            {
                _context.Examinations.Add(examination);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(Model.Examination examination)
        {
            try
            {
                var ex = SelectById(examination.ExaminationId);
                if (ex == null)
                {
                    throw new KeyNotFoundException("Not Found");
                }
                _context.Entry(ex).State = EntityState.Modified;
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
                var ex = SelectById(id);
                if (ex == null)
                {
                    throw new KeyNotFoundException("Not Found");
                }
                _context.Examinations.Remove(ex);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GenerateExamination(Model.Examination examination, IList<Question> questions = null)
        {
            try
            {
                //if (_context.ExaminationQuestion.Any(a => a.Examination.ExaminationId == examination.ExaminationId ))
                //{
                //    return "Duplicated insert";
                //}
                //todo: validate does examination has been created。
                if (questions == null)
                {
                    questions = _context.Questions.ToList();
                }

                if (examination.QuestionCount >= questions.Count)
                {
                    return "Question count should greater than examination count";
                }

                var result = examination.GenerateQuestions(questions);
                foreach (var question in result)
                {
                    var eq = new ExaminationQuestion
                    {
                        Examination = examination,
                        Question = question
                    };
                    _context.ExaminationQuestion.Add(eq);
                }
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
}