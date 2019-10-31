using FreshMvvm;

namespace bonus.app.PageModel
{
    public abstract class BaseMainPageModel : FreshBasePageModel
    {
        public abstract void LoadData();

        public bool IsLoaded
        {
            get;
            protected set;
        }
    }
}
