using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.PageModel;
using FreshMvvm;
using Xamarin.Forms;

namespace bonus.app
{
    public class MainTabbedPage : TabbedPage, IFreshNavigationService
    {
        #region Data
        #region Fields
        /// <summary>
        /// Список страниц отображаемых в меню.
        /// </summary>
        private readonly List<TabbedPage> _tabs = new List<TabbedPage>();
        #endregion
        #endregion

        #region .ctor
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MainTabbedPage" />, регистрируя сервис навигации.
        /// </summary>
        public MainTabbedPage(string navigationServiceName)
        {
            CurrentPageChanged += CurrentPageHasChanged;
            NavigationServiceName = navigationServiceName;
            RegisterNavigation();
        }
        #endregion


        #region IFreshNavigationService members
        /// <summary>
        /// Возвращает название сервиса навигации.
        /// </summary>
        public string NavigationServiceName
        {
            get;
        }

        /// <summary>
        /// Уведомляет потомков об остановке. 
        /// </summary>
        public void NotifyChildrenPageWasPopped()
        {
            foreach (var page in Children)
            {
                if (page is NavigationPage navigationPage)
                {
                    navigationPage.NotifyAllChildrenPopped();
                }
            }
        }

        /// <summary>
        /// Покидает текущую страницу.
        /// </summary>
        /// <param name="modal">Покинуть ли страницу модально?</param>
        /// <param name="animate">Покинуть ли страницу с анимацией?</param>
        /// <returns>Возвращает операцию.</returns>
        public Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
            {
                return CurrentPage.Navigation.PopModalAsync(animate);
            }

            return CurrentPage.Navigation.PopAsync(animate);
        }

        /// <summary>
        /// Возвращает к корневой странице.
        /// </summary>
        /// <param name="animate">Вернуть ли с анимацией?</param>
        /// <returns>Возвращает операцию.</returns>
        public Task PopToRoot(bool animate = true) => CurrentPage.Navigation.PopToRootAsync(animate);


        /// <summary>
        /// Переключает выбранную корневую модель страницы.
        /// </summary>
        /// <typeparam name="T">Тип модели представления страницы</typeparam>
        /// <returns>Возвращает операцию.</returns>
        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            var page = _tabs.FindIndex(o => o.GetModel()
                                             .GetType()
                                             .FullName ==
                                            typeof(T).FullName);

            if (page > -1)
            {
                CurrentPage = Children[page];
                var topOfStack = CurrentPage.Navigation.NavigationStack.LastOrDefault();
                if (topOfStack != null)
                {
                    return Task.FromResult(topOfStack.GetModel());
                }
            }

            return null;
        }
        #endregion

        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            if ((sender as TabbedPage)?.CurrentPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.BindingContext is BaseMainPageModel pageModel)
                {
                    if (pageModel.IsLoaded)
                    {
                        return;
                    }
                    pageModel.LoadData();
                }
            }
        }

        #region Private
        /// <summary>
        /// Регистрирует сервис навигации.
        /// </summary>
        private void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public Task PushPage(Xamarin.Forms.Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
