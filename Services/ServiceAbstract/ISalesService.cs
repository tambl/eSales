using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceAbstract
{
    public interface ISalesService
    {
        #region Consultants
        List<ConsultantDto> GetConsultants();
        bool CreateConsultant(ConsultantDto consultant);
        ConsultantDto GetConsultantById(int id);
        bool EditConultant(ConsultantDto consultant);
        bool DeleteConsultant(int id);
        bool CheckIsConsultantRecommender(int? id, string personalNo);
        
        #endregion

    }
}
