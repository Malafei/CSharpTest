using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            // одразу вираховуєм кінцеву дату
            DateTime endDate = startDate.AddDays(dayCount - 1);
            // якщо вихідних нема повертаємо цю дату
            if (weekEnds == null)
                return endDate;

            int count = dayCount - 1; // створили тимчасовий лічильник для підрахунку вихідних
            for (int i = 0; i < weekEnds.Length; i++)
            {
                //первіряєм якщо початок поза діапазоном а кінець в діапазоні і так само якщо початок в діапазоні а кінець поза діапазоном міняєм вихідні
                if (startDate > weekEnds[i].StartDate && startDate <= weekEnds[i].EndDate)
                    weekEnds[i] = new WeekEnd(startDate, weekEnds[i].EndDate);

                //перевіряєм якщо вихідні знаходяться в діапазоні то 
                if (startDate <= weekEnds[i].StartDate && endDate >= weekEnds[i].EndDate ||
                    endDate >= weekEnds[i].StartDate && endDate < weekEnds[i].EndDate)
                {
                    //рахуєм скільки днів вихідних
                    count += (int.Parse(weekEnds[i].EndDate.Subtract(weekEnds[i].StartDate).Days.ToString()) + 1);
                    endDate = startDate.AddDays(count); //міняєм кінцеву дату з урахуванням вихідних
                }
            }

            return endDate;//повертаєм результат
        }
    }
}
