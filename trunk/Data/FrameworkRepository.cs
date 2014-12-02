using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
namespace Data
{

    public class FrameworkRepository<TEntityObject> where TEntityObject : EntityObject
    {
        private ObjectSet<TEntityObject> mObjectSet;
        private KaraokeEntities mKaraokeEntities;
        private List<MyEntityObject> mListEntityObject;
        public FrameworkRepository(KaraokeEntities kara)
        {
            mKaraokeEntities = kara;
            mObjectSet = mKaraokeEntities.CreateObjectSet<TEntityObject>();
            Init();
        }
        public FrameworkRepository(KaraokeEntities kara, ObjectSet<TEntityObject> objectSet)
        {
            mKaraokeEntities = kara;
            mObjectSet = objectSet;
            Init();
        }
        private void Init()
        {
            mListEntityObject = new List<MyEntityObject>();
            mKaraokeEntities.ContextOptions.LazyLoadingEnabled = false;
            mObjectSet.MergeOption = MergeOption.NoTracking;
        }
        public static IQueryable<TEntityObject> QueryAppendOnly(ObjectSet<TEntityObject> objectSet, MergeOption mergeOption)
        {
            objectSet.MergeOption = mergeOption;
            return objectSet;
        }
        public static IQueryable<TEntityObject> QueryNoTracking(ObjectSet<TEntityObject> objectSet)
        {
            objectSet.MergeOption = MergeOption.NoTracking;
            return objectSet;
        }
        public IQueryable<TEntityObject> Query()
        {
            return mObjectSet;
        }
        public TEntityObject GetAttachEntityObject(TEntityObject obj)
        {
            if (obj.EntityKey != null)
            {
                object rObj;
                if (mKaraokeEntities.TryGetObjectByKey(obj.EntityKey, out rObj))
                {
                    return (TEntityObject)rObj;
                }
            }
            return null;
        }
        private void MakeChangeEntityObject(MyEntityObject obj)
        {
            switch (obj.Type)
            {
                case FrameworkRepository<TEntityObject>.MyEntityObjectType.Added:
                    mObjectSet.AddObject(obj.EntityObject);
                    break;
                case FrameworkRepository<TEntityObject>.MyEntityObjectType.Edit:
                    this.Attach(obj.EntityObject);
                    mKaraokeEntities.ObjectStateManager.ChangeObjectState(obj.EntityObject, System.Data.EntityState.Modified);
                    break;
                case FrameworkRepository<TEntityObject>.MyEntityObjectType.Delete:
                    this.Attach(obj.EntityObject);
                    mObjectSet.DeleteObject(obj.EntityObject);
                    break;
                default:
                    break;
            }
        }
        public void AddObject(TEntityObject obj)
        {
            this.AddMyEntityObject(new MyEntityObject(MyEntityObjectType.Added, obj));
        }
        public void Update(TEntityObject obj)
        {
            this.AddMyEntityObject(new MyEntityObject(MyEntityObjectType.Edit, obj));
        }
        public void DeleteObject(TEntityObject obj)
        {            
            this.AddMyEntityObject(new MyEntityObject(MyEntityObjectType.Delete, obj));
        }
        private void AddMyEntityObject(MyEntityObject obj)
        {            
            if (!mListEntityObject.Contains(obj))
            {
                mListEntityObject.Add(obj);
            }
        }
        private void Attach(TEntityObject obj)
        {
            if (obj.EntityState == System.Data.EntityState.Detached)
            {
                mObjectSet.Attach(obj);
            }
        }
        private void Detach(TEntityObject obj)
        {
            if (obj.EntityState != System.Data.EntityState.Detached)
            {
                mObjectSet.Detach(obj);
            }
        }
        public void Commit()
        {
            foreach (var item in mListEntityObject)
            {
                this.MakeChangeEntityObject(item);
            }
            mKaraokeEntities.SaveChanges();
            foreach (var item in mListEntityObject)
            {
                this.Detach(item.EntityObject);
            }
            mListEntityObject.Clear();
        }
        public void Refresh()
        {
            mListEntityObject.Clear();
        }
        private class MyEntityObject
        {
            public MyEntityObjectType Type { get; set; }
            public TEntityObject EntityObject { get; set; }
            public MyEntityObject(MyEntityObjectType type, TEntityObject obj)
            {
                this.Type = type;
                this.EntityObject = obj;
            }
        }
        private enum MyEntityObjectType
        {
            Added,
            Edit,
            Delete
        }
    }
}
