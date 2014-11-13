using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
namespace Data
{
    
    internal class FrameworkRepository<TEntityObject> where TEntityObject : EntityObject
    {        
        private ObjectSet<TEntityObject> mObjectSet;
        private KaraokeEntities mKaraokeEntities;
        private List<TEntityObject> mListEntityObjectAttact;        
        public FrameworkRepository(KaraokeEntities kara, ObjectSet<TEntityObject> objectSet)
        {
            mListEntityObjectAttact = new List<TEntityObject>();
            mKaraokeEntities = kara;
            mKaraokeEntities.ContextOptions.LazyLoadingEnabled = false;
            mObjectSet = objectSet;
            mObjectSet.MergeOption = MergeOption.NoTracking;              
        }
        public static IQueryable<TEntityObject> QueryNoTracking(ObjectSet<TEntityObject> objectSet)
        {
            return objectSet;
        }
        public IQueryable<TEntityObject> Query()
        {
            return mObjectSet;
        }
        public TEntityObject GetAttachEntityObject(TEntityObject obj)
        {
            if (obj.EntityKey!=null)
            {
                object rObj;
                if (mKaraokeEntities.TryGetObjectByKey(obj.EntityKey,out rObj))
                {                    
                    return (TEntityObject)rObj;
                }                
            }
            return null;
        }
        public void AddObject(TEntityObject obj)
        {                        
            mObjectSet.AddObject(obj);
            this.AddEntityObjectAttact(obj);
        }
        public void Update(TEntityObject obj)
        {
            this.Attach(obj);
            mKaraokeEntities.ObjectStateManager.ChangeObjectState(obj, System.Data.EntityState.Modified);
            this.AddEntityObjectAttact(obj);
        }
        public void DeleteObject(TEntityObject obj)
        {
            this.Attach(obj);
            mObjectSet.DeleteObject(obj);
            this.AddEntityObjectAttact(obj);
        }
        private void AddEntityObjectAttact(TEntityObject obj)
        {
            if (!mListEntityObjectAttact.Contains(obj))
            {
                mListEntityObjectAttact.Add(obj);
            }
        }
        private void Attach(TEntityObject obj)
        {
            if (obj.EntityState==System.Data.EntityState.Detached)
            {
                mObjectSet.Attach(obj);
            }
        }
        private void Detach(TEntityObject obj)
        {
            if (obj.EntityState!=System.Data.EntityState.Detached)
            {
                mObjectSet.Detach(obj);
            }
        }
        public void Commit()
        {
            mKaraokeEntities.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            foreach (TEntityObject item in mListEntityObjectAttact)
            {
                if (item.EntityState!=System.Data.EntityState.Detached)
                {
                    mKaraokeEntities.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Detached);
                }
            }
            mListEntityObjectAttact.Clear();
        }
        public void Refresh(TEntityObject obj)
        {
            this.Attach(obj);
            mKaraokeEntities.Refresh(RefreshMode.StoreWins, obj);
            this.Detach(obj);
        }
        public void Refresh(IEnumerable<TEntityObject> list)
        {
            foreach (var item in list)
            {
                this.Attach(item);
            }
            mKaraokeEntities.Refresh(RefreshMode.StoreWins, list);
            foreach (var item in list)
            {
                this.Detach(item);
            }
        }
    }
}
