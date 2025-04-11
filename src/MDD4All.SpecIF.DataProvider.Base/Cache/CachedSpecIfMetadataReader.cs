using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MDD4All.SpecIF.DataProvider.Base.Cache
{
    public class CachedSpecIfMetadataReader : AbstractSpecIfMetadataReader
    {
        private static Dictionary<Key, DataType> DataTypesCache = null;
        private static Dictionary<Key, PropertyClass> PropertyClassesCache = null;
        private static Dictionary<Key, ResourceClass> ResourceClassesCache = null;
        private static Dictionary<Key, StatementClass> StatementClassesCache = null;
        private static Dictionary<Key, DiagramObjectClass> DiagramClassesCache = null;

        private static Dictionary<string, PropertyClass> RevisionlessPropertyClasses = null;
        private static Dictionary<string, ResourceClass> RevisionlessResourceClasses = null;
        private static Dictionary<string, StatementClass> RevisionlessStatementClasses = null;
        private static Dictionary<string, DataType> RevisionlessDataTypes = null;

        private ISpecIfMetadataReader _metadataReader;

        public CachedSpecIfMetadataReader(ISpecIfMetadataReader metadataReader)
        {
            _metadataReader = metadataReader;

            if (!IsInitialized)
            {
                InitializeMetadataCache();
            }
        }

        public void ReinitializeCache()
        {
            InitializeMetadataCache();
        }

        private bool IsInitialized
        {
            get
            {
                return DataTypesCache != null;
            }
        }

        private void InitializeMetadataCache()
        {
            RevisionlessDataTypes = new Dictionary<string, DataType>();
            DataTypesCache = new Dictionary<Key, DataType>();

            List<DataType> dataTypes = _metadataReader.GetAllDataTypes();

            foreach (DataType dataType in dataTypes)
            {
                DataTypesCache.Add(new Key(dataType.ID, dataType.Revision), dataType);

                if (!RevisionlessDataTypes.ContainsKey(dataType.ID))
                {
                    List<DataType> dataTypesWithSameId = dataTypes.FindAll(element => element.ID == dataType.ID);
                    DataType latestDataType = dataTypesWithSameId.OrderBy(element => element.ChangedAt).ToList()[0];
                    RevisionlessDataTypes.Add(dataType.ID, latestDataType);
                }
            }

            RevisionlessPropertyClasses = new Dictionary<string, PropertyClass>();
            PropertyClassesCache = new Dictionary<Key, PropertyClass>();

            List<PropertyClass> propertyClasses = _metadataReader.GetAllPropertyClasses();

            foreach (PropertyClass propertyClass in propertyClasses)
            {
                PropertyClassesCache.Add(new Key(propertyClass.ID, propertyClass.Revision), propertyClass);
                if (!RevisionlessPropertyClasses.ContainsKey(propertyClass.ID))
                {
                    List<PropertyClass> propertyClassWithSameId = propertyClasses.FindAll(element => element.ID == propertyClass.ID);
                    PropertyClass latestPropertyClass = propertyClassWithSameId.OrderBy(element => element.ChangedAt).ToList()[0];
                    RevisionlessPropertyClasses.Add(propertyClass.ID, latestPropertyClass);
                }
            }

            RevisionlessResourceClasses = new Dictionary<string, ResourceClass>();
            ResourceClassesCache = new Dictionary<Key, ResourceClass>();

            List<ResourceClass> ResourceClasses = _metadataReader.GetAllResourceClasses();
            foreach (ResourceClass resourceClass in ResourceClasses)
            {
                ResourceClassesCache.Add(new Key(resourceClass.ID, resourceClass.Revision), resourceClass);
                if (!RevisionlessResourceClasses.ContainsKey(resourceClass.ID))
                {
                    List<ResourceClass> ResourceClassWithSameId = ResourceClasses.FindAll(element => element.ID == resourceClass.ID);
                    ResourceClass latestResourceClass = ResourceClassWithSameId.OrderBy(element => element.ChangedAt).ToList()[0];
                    RevisionlessResourceClasses.Add(resourceClass.ID, latestResourceClass);
                }
            }

            RevisionlessStatementClasses = new Dictionary<string, StatementClass>();
            StatementClassesCache = new Dictionary<Key, StatementClass>();

            List<StatementClass> StatementClasses = _metadataReader.GetAllStatementClasses();
            foreach (StatementClass statementClass in StatementClasses)
            {
                StatementClassesCache.Add(new Key(statementClass.ID, statementClass.Revision), statementClass);
                if (!RevisionlessStatementClasses.ContainsKey(statementClass.ID))
                {
                    List<StatementClass> StatementClassWithSameId = StatementClasses.FindAll(element => element.ID == statementClass.ID);
                    StatementClass latestStatementClass = StatementClassWithSameId.OrderBy(element => element.ChangedAt).ToList()[0];
                    RevisionlessStatementClasses.Add(statementClass.ID, latestStatementClass);
                }
            }

            DiagramClassesCache = new Dictionary<Key, DiagramObjectClass>();

            List<DiagramObjectClass> diagramObjectClasses = _metadataReader.GetAllDiagramObjectClasses();
            foreach (DiagramObjectClass diagramObjectClass in diagramObjectClasses)
            {
                DiagramClassesCache.Add(new Key(diagramObjectClass.ID, diagramObjectClass.Revision), diagramObjectClass);

            }
        }

        public override List<DataType> GetAllDataTypeRevisions(string dataTypeID)
        {
            List<DataType> result = new List<DataType>();

            foreach(KeyValuePair<Key, DataType> keyValuePair in DataTypesCache)
            {
                if(keyValuePair.Key.ID == dataTypeID)
                {
                    result.Add(keyValuePair.Value);
                }
            }

            return result;
        }

        public override List<DataType> GetAllDataTypes()
        {
            List<DataType> result = new List<DataType>();

            foreach(KeyValuePair<Key, DataType> keyValuePair in DataTypesCache)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public override List<PropertyClass> GetAllPropertyClasses()
        {
            List<PropertyClass> result = new List<PropertyClass>();

            foreach (KeyValuePair<Key, PropertyClass> keyValuePair in PropertyClassesCache)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public override List<PropertyClass> GetAllPropertyClassRevisions(string propertyClassID)
        {
            List<PropertyClass> result = new List<PropertyClass>();

            foreach (KeyValuePair<Key, PropertyClass> keyValuePair in PropertyClassesCache)
            {
                if (keyValuePair.Key.ID == propertyClassID)
                {
                    result.Add(keyValuePair.Value);
                }
            }

            return result;
        }

        public override List<ResourceClass> GetAllResourceClasses()
        {
            List<ResourceClass> result = new List<ResourceClass>();

            foreach (KeyValuePair<Key, ResourceClass> keyValuePair in ResourceClassesCache)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public override List<ResourceClass> GetAllResourceClassRevisions(string resourceClassID)
        {
            List<ResourceClass> result = new List<ResourceClass>();

            foreach (KeyValuePair<Key, ResourceClass> keyValuePair in ResourceClassesCache)
            {
                if (keyValuePair.Key.ID == resourceClassID)
                {
                    result.Add(keyValuePair.Value);
                }
            }

            return result;
        }

        public override List<StatementClass> GetAllStatementClasses()
        {
            List<StatementClass> result = new List<StatementClass>();

            foreach (KeyValuePair<Key, StatementClass> keyValuePair in StatementClassesCache)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public override List<StatementClass> GetAllStatementClassRevisions(string statementClassID)
        {
            List<StatementClass> result = new List<StatementClass>();

            foreach (KeyValuePair<Key, StatementClass> keyValuePair in StatementClassesCache)
            {
                if (keyValuePair.Key.ID == statementClassID)
                {
                    result.Add(keyValuePair.Value);
                }
            }

            return result;
        }

        public override DataType GetDataTypeByKey(Key key)
        {
            DataType result = null;

            if (!string.IsNullOrEmpty(key.ID))
            {
                if (!string.IsNullOrEmpty(key.Revision))
                {
                    if (DataTypesCache.ContainsKey(key))
                    {
                        result = DataTypesCache[key];
                    }
                }
                else
                {
                    if (RevisionlessDataTypes.ContainsKey(key.ID))
                    {
                        result = RevisionlessDataTypes[key.ID];
                    }
                }
            }

            return result;
        }



        public override string GetLatestPropertyClassRevision(string propertyClassID)
        {
            throw new NotImplementedException();
        }

        public override string GetLatestResourceClassRevision(string resourceClassID)
        {
            throw new NotImplementedException();
        }

        public override string GetLatestStatementClassRevision(string statementClassID)
        {
            throw new NotImplementedException();
        }

        public override PropertyClass GetPropertyClassByKey(Key key)
        {
            PropertyClass result = null;

            if (!string.IsNullOrEmpty(key.ID))
            {
                if (!string.IsNullOrEmpty(key.Revision))
                {
                    if (PropertyClassesCache.ContainsKey(key))
                    {
                        result = PropertyClassesCache[key];
                    }
                }
                else
                {
                    if (RevisionlessPropertyClasses.ContainsKey(key.ID))
                    {
                        result = RevisionlessPropertyClasses[key.ID];
                    }
                }
            }


            return result;
        }

        public override ResourceClass GetResourceClassByKey(Key key)
        {
            ResourceClass result = null;

            if (!string.IsNullOrEmpty(key.ID))
            {
                if (!string.IsNullOrEmpty(key.Revision))
                {
                    if (ResourceClassesCache.ContainsKey(key))
                    {
                        result = ResourceClassesCache[key];
                    }
                }
                else
                {
                    if (RevisionlessResourceClasses.ContainsKey(key.ID))
                    {
                        result = RevisionlessResourceClasses[key.ID];
                    }
                }
            }
            return result;
        }

        public override StatementClass GetStatementClassByKey(Key key)
        {
            StatementClass result = null;

            if (!string.IsNullOrEmpty(key.ID))
            {
                if (!string.IsNullOrEmpty(key.Revision))
                {
                    if (StatementClassesCache.ContainsKey(key))
                    {
                        result = StatementClassesCache[key];
                    }

                }
                else
                {
                    if (RevisionlessStatementClasses.ContainsKey(key.ID))
                    {
                        result = RevisionlessStatementClasses[key.ID];
                    }
                }
            }

            return result;
        }

        public override void NotifyMetadataChanged()
        {
            ReinitializeCache();
        }

        public override List<DiagramObjectClass> GetAllDiagramObjectClasses()
        {
            List<DiagramObjectClass> result = new List<DiagramObjectClass>();

            foreach (KeyValuePair<Key, DiagramObjectClass> keyValuePair in DiagramClassesCache)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public override DiagramObjectClass GetDiagramObjectClassByKey(Key key)
        {
            DiagramObjectClass result = null;

            if (!string.IsNullOrEmpty(key.ID))
            {
                if (!string.IsNullOrEmpty(key.Revision))
                {
                    if (DiagramClassesCache.ContainsKey(key))
                    {
                        result = DiagramClassesCache[key];
                    }

                }
                else
                {
                    //if (RevisionlessStatementClasses.ContainsKey(key.ID))
                    //{
                    //    result = RevisionlessStatementClasses[key.ID];
                    //}
                }
            }

            return result;
        }

        public override List<DiagramObjectClass> GetAllDiagramObjectClassesRevisions(string classID)
        {
            throw new NotImplementedException();
        }
    }
}
