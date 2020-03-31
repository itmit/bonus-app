using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	class ShareService : BaseService, IShareService
	{
		private const string GetAllUri = "";

		public async Task<IEnumerable<Share>> GetAll()
		{
			return new List<Share>
			{
				new  Share
				{
					Name = "1 + 1 = 2!",
					Description = "СХОДИ НА СТРИЖКУ ДВАЖДЫ И ПОЛУЧИ СКИДКУ НА ПЕРВУЮ*!\n\n\n*для получения скидки на первую стрижку необходимо подстричься у нас дважды, поэтому получить скидку невозможно*.\n\nДеньги за стрижку не возвращаем.",
					ShortDescription = "СХОДИ НА СТРИЖКУ ДВАЖДЫ И ПОЛУЧИ СКИДКУ НА ПЕРВУЮ*!",
					ShareTime = DateTime.Today,
					ImageSource = "chika",
					Status = "Завершена"
				}
			};

			// TODO: Удалить код выше и оставить кода будет готова api, и будет ссылка.
			return await GetAsync<IEnumerable<Share>>(GetAllUri);
		}

		public ShareService(IAuthService authService)
			: base(authService)
		{
		}
	}
}
