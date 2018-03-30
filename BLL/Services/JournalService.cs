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
        private JournalRepository _journalRepository;
        public JournalService(string connectionString)
        {
            _journalRepository = new JournalRepository(connectionString);
        }
        public async Task CreateAsync(CreateJournalViewModel inputItem)
        {
            Journal tempJournal = Mapper.Map<Journal>(inputItem);
            await _journalRepository.CreateAsync(tempJournal);
        }
        public async Task<List<JournalViewModel>> GetAllAsync()
        {
            var returnValue = await _journalRepository.GetAllAsync();
            return returnValue.ToList();
        }
        public async Task DeleteAsync(int Id)
        {
            await _journalRepository.RemoveAsync(Id);
        }
        public async Task UpdateAsync(JournalViewModel inputJournalModel)
        {
            var tempJournal = new Journal();
            Mapper.Map(inputJournalModel, tempJournal);
            await _journalRepository.UpdateAsync(tempJournal);
        }
        public async Task<JournalViewModel> GetAsync(int id)
        {
            var tempJournal = await  _journalRepository.FindByIdAsync(id);

            var result = Mapper.Map<JournalViewModel>(tempJournal);

            return result;
        }
        public async Task<Boolean> DeleteRangeAsync(List<int> id)
        {
            var result = await _journalRepository.RemoveAsync(id);
            return result;
        }
        public async Task<List<JournalViewModel>> FirndByTitleAsync(string partialTitle)
        {
            var resultQuery = await _journalRepository.FirndByTitleAsync(partialTitle);
            var result = Mapper.Map<List<JournalViewModel>>(resultQuery);
            return result;
        }
    }
}
