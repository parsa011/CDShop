using Shop.Data.DatabaseContext;
using Shop.Domain.Entities;
using Sop.Data.Inrefaces;
using Sop.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.UnitOfWork
{
    public class UnitOfWork
    {
        #region ctor
        private readonly ShopDbContext _db;
        public UnitOfWork(ShopDbContext db)
        {
            _db = db;
        }
        #endregion ctor

        #region repositories
        private IGenericRepository<User> _usersGenericRepository;
        public IGenericRepository<User> UsersGenericRepository
        {
            get
            {
                if (_usersGenericRepository == null)
                {
                    _usersGenericRepository = new GenericRepository<User>(_db);
                }
                return _usersGenericRepository;
            }
        }

        private IGenericRepository<Category> _categoriesGenericRepository;
        public IGenericRepository<Category> CategoriesGenericRepository
        {
            get
            {
                if (_categoriesGenericRepository == null)
                {
                    _categoriesGenericRepository = new GenericRepository<Category>(_db);
                }
                return _categoriesGenericRepository;
            }
        }

        private IGenericRepository<Comment> _commentsGenericRepository;
        public IGenericRepository<Comment> CommentsGenericRepository
        {
            get
            {
                if (_commentsGenericRepository == null)
                {
                    _commentsGenericRepository = new GenericRepository<Comment>(_db);
                }
                return _commentsGenericRepository;
            }
        }

        private IGenericRepository<Role> _rolesGenericRepository;
        public IGenericRepository<Role> RolesGenericRepository
        {
            get
            {
                if (_rolesGenericRepository == null)
                {
                    _rolesGenericRepository = new GenericRepository<Role>(_db);
                }
                return _rolesGenericRepository;
            }
        }

        private IGenericRepository<Order> _ordersGenericRepository;
        public IGenericRepository<Order> OrdersGenericRepository
        {
            get
            {
                if (_ordersGenericRepository == null)
                {
                    _ordersGenericRepository = new GenericRepository<Order>(_db);
                }
                return _ordersGenericRepository;
            }
        }

        private IGenericRepository<OrderDetail> _orderDetailsGenericRepository;
        public IGenericRepository<OrderDetail> OrderDetailsGenericRepository
        {
            get
            {
                if (_orderDetailsGenericRepository == null)
                {
                    _orderDetailsGenericRepository = new GenericRepository<OrderDetail>(_db);
                }
                return _orderDetailsGenericRepository;
            }
        }

        private IGenericRepository<Product> _productsGenericRepository;
        public IGenericRepository<Product> ProductsGenericRepository
        {
            get
            {
                if (_productsGenericRepository == null)
                {
                    _productsGenericRepository = new GenericRepository<Product>(_db);
                }
                return _productsGenericRepository;
            }
        }

        private IGenericRepository<ProductImage> _productImagesGenericRepository;
        public IGenericRepository<ProductImage> ProductImagesGenericRepository
        {
            get
            {
                if (_productImagesGenericRepository == null)
                {
                    _productImagesGenericRepository = new GenericRepository<ProductImage>(_db);
                }
                return _productImagesGenericRepository;
            }
        }

        private IGenericRepository<Address> _addressesGenericRepository;
        public IGenericRepository<Address> AddressesGenericRepository
        {
            get
            {
                if (_addressesGenericRepository == null)
                {
                    _addressesGenericRepository = new GenericRepository<Address>(_db);
                }
                return _addressesGenericRepository;
            }
        }
        #endregion repositories

        #region actions
        public void Save()
        {
            _db.SaveChanges();
        }


        public void Dispose()
        {
            this.Dispose();
        }
        #endregion
    }
}
