using System.Collections.Generic;
using System.Linq;

namespace BingLibrary.hjb
{
    public class PagabledCollection<T> : DataSource
    {
        #region 私有字段

        private IEnumerable<T> _pagedItems = null;
        private int _totalItems = default(int);
        private int _currentPage = default(int);
        private int _pageSize = 5;
        private int _totalPages;
        private bool _canPageUp;
        private bool _canPageDown;
        private IEnumerable<T> _containerItems;

        #endregion 私有字段

        #region 私有方法

        /// <summary>
        /// 计算总页面数
        /// </summary>
        private void ComputePages()
        {
            int p = TotalItems / PageSize;
            if ((TotalItems % PageSize) > 0)
            {
                p++;
            }
            TotalPages = p;
            SetPagedItemsCore();
        }

        /// <summary>
        /// 设置分页后某页的项列表
        /// </summary>
        private void SetPagedItemsCore()
        {
            var r = _containerItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            PagedItems = r;
        }

        /// <summary>
        /// 检查是否可以操作“上一页”、“下一页”
        /// </summary>
        private void CheckPaging()
        {
            CanPageUp = (_currentPage == 1) ? false : true;
            CanPageDown = (_currentPage == _totalPages) ? false : true;
        }

        #endregion 私有方法

        #region 构造函数

        public PagabledCollection(IEnumerable<T> srcItems)
        {
            _containerItems = srcItems;
            // 总项目数
            _totalItems = _containerItems.Count();
            // 计算总页数
            ComputePages();

            CurrentPage = 1; //默认页
        }

        #endregion 构造函数

        #region 属性

        /// <summary>
        /// 每页显示项数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value == 0)
                {
                    return;
                }
                if (value != _pageSize)
                {
                    _pageSize = value;
                    ComputePages(); //重新计算页数
                    CheckPaging();
                    SetPagedItemsCore();
                    CurrentPage = 1;

                    NotifyPropertyChanged(nameof(PageSize));
                }
            }
        }

        /// <summary>
        /// 总项数
        /// </summary>
        public int TotalItems { get { return _totalItems; } }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return _totalPages; }
            private set
            {
                if (value != _totalPages)
                {
                    _totalPages = value;
                    NotifyPropertyChanged(nameof(TotalPages));
                }
            }
        }

        /// <summary>
        /// 是否可以向前翻页
        /// </summary>
        public bool CanPageUp
        {
            get { return _canPageUp; }
            private set
            {
                if (value != _canPageUp)
                {
                    _canPageUp = value;
                    NotifyPropertyChanged(nameof(CanPageUp));
                }
            }
        }

        /// <summary>
        /// 是否可以向后翻页
        /// </summary>
        public bool CanPageDown
        {
            get { return _canPageDown; }
            private set
            {
                if (value != _canPageDown)
                {
                    _canPageDown = value;
                    NotifyPropertyChanged(nameof(CanPageDown));
                }
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    if (value < 1) _currentPage = 1;
                    else if (value > TotalPages) _currentPage = TotalPages;
                    else _currentPage = value;
                    // 筛选内容
                    SetPagedItemsCore();
                    CheckPaging();
                    NotifyPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        public IEnumerable<T> PagedItems
        {
            get { return _pagedItems; }
            private set
            {
                if (value != _pagedItems)
                {
                    _pagedItems = value;
                    NotifyPropertyChanged(nameof(PagedItems));
                }
            }
        }

        #endregion 属性
    }
}