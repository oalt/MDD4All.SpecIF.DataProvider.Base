using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataProvider.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MDD4All.SpecIF.DataProvider.Base.Cache
{
    public class MetadataCache
    {
        public static Dictionary<Key, DataType> DataTypesCache = null;
        public static Dictionary<Key, PropertyClass> PropertyClassesCache = null;
        public static Dictionary<Key, ResourceClass> ResourceClassesCache = null;
        public static Dictionary<Key, StatementClass> StatementClassesCache = null;

        public static Dictionary<string, PropertyClass> RevisionlessPropertyClasses = null;
        public static Dictionary<string, ResourceClass> RevisionlessResourceClasses = null;
        public static Dictionary<string, StatementClass> RevisionlessStatementClasses = null;
        public static Dictionary<string, DataType> RevisionlessDataTypes = null;

        public static void Initialize(ISpecIfMetadataReader metadataReader)
        {
            RevisionlessDataTypes = new Dictionary<string, DataType>();
            DataTypesCache = new Dictionary<Key, DataType>();

            List<DataType> dataTypes = metadataReader.GetAllDataTypes();

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

            List<PropertyClass> propertyClasses = metadataReader.GetAllPropertyClasses();

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

            List<ResourceClass> ResourceClasses = metadataReader.GetAllResourceClasses();
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

            List<StatementClass> StatementClasses = metadataReader.GetAllStatementClasses();
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
        }

        public static void Clear()
        {
            DataTypesCache = null;
        }

        public static bool IsInitialized()
        {
            return DataTypesCache != null;
        }


    }
}
