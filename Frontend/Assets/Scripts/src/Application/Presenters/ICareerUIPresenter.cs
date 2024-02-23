using System.Collections.Generic;
using TechnicalEvaluation.Domain.CareerAggregate;

namespace TechnicalEvaluation.Application.Presenters
{
    public interface ICareerUIPresenter
    {
        public void Render(Career career);

        public void ClearContents();
    }
}