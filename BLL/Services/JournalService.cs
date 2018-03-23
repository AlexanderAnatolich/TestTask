using DAL.DataContext;
using DAL.Repositories;
using DAL.Models;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    
    public class JournalService
    {
        private DataContex _modelsContext;
        private JournalRepository _journalRepository;
        public JournalService(string connectionString)
        {
            _modelsContext = new DataContex(connectionString);
            _journalRepository = new JournalRepository(_modelsContext);
        }
        public void Create(CreateJournalViewModel inputItem)
        {
            Journal tempJournal = Mapper.Map<Journal>(inputItem);
            tempJournal.PublishHouse = null;
            _journalRepository.Create(tempJournal);
        }
        public List<JournalViewModel> GetAll()
        {
            IEnumerable<Journal> tempJournal = _journalRepository.Get();
            var returnValue = Mapper.Map<List<JournalViewModel>>(tempJournal);
            return returnValue;
        }
        public void Delete(int Id)
        {
            var temJournal = _journalRepository.FindById(Id);
            _journalRepository.Remove(temJournal);
        }
        public void Update(JournalViewModel inputJournalModel)
        {
            var tempJournal = new Journal();
            Mapper.Map(inputJournalModel, tempJournal);
            _journalRepository.Update(tempJournal);
        }
        public JournalViewModel Get(int? id)
        {
            Journal tempJournal = _journalRepository.FindById(id);
            JournalViewModel result = new JournalViewModel();

            Mapper.Map(tempJournal, result);

            return result;
        }
        public async Task<Boolean> DeleteRange(List<int> id)
        {
            var result = await _journalRepository.RemoveRangeAsync(x => id.Contains(x.Id));
            return result;
        }
    }
}
