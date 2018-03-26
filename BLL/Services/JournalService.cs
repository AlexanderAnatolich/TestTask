using DAL.DataContext;
using DAL.Repositories;
using DAL.Models;
using Model.Models;
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
        public async Task CreateAsync(CreateJournalViewModel inputItem)
        {
            Journal tempJournal = Mapper.Map<Journal>(inputItem);
            await _journalRepository.CreateAsync(tempJournal);
        }
        public async Task<List<JournalViewModel>> GetAllAsync()
        {
            IEnumerable<Journal> tempJournal = await _journalRepository.GetAsync();
            var returnValue = Mapper.Map<List<JournalViewModel>>(tempJournal);
            return returnValue;
        }
        public async Task DeleteAsync(int Id)
        {
            var temJournal = _journalRepository.FindById(Id);
            await _journalRepository.RemoveAsync(temJournal);
        }
        public async Task UpdateAsync(JournalViewModel inputJournalModel)
        {
            var tempJournal = new Journal();
            Mapper.Map(inputJournalModel, tempJournal);
            await _journalRepository.UpdateAsync(tempJournal);
        }
        public JournalViewModel Get(int? id)
        {
            Journal tempJournal = _journalRepository.FindById(id);
            JournalViewModel result = new JournalViewModel();

            Mapper.Map(tempJournal, result);

            return result;
        }
        public async Task<Boolean> DeleteRangeAsync(List<int> id)
        {
            var result = await _journalRepository.RemoveRangeAsync(x => id.Contains(x.Id));
            return result;
        }
    }
}
