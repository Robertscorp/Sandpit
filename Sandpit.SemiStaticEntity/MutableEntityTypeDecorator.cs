//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace Sandpit.SemiStaticEntity
//{

//    public class MutableEntityTypeDecorator : IMutableEntityType
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IMutableEntityType m_MutableEntityType;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public MutableEntityTypeDecorator(IMutableEntityType mutableEntityType)
//            => this.m_MutableEntityType = mutableEntityType ?? throw new ArgumentNullException(nameof(mutableEntityType));

//        #endregion Constructors

//        public object this[string name] { get => this.m_MutableEntityType[name]; set => this.m_MutableEntityType[name] = value; }

//        object IAnnotatable.this[string name] => ((IAnnotatable)this.m_MutableEntityType)[name];

//        public IMutableModel Model => this.m_MutableEntityType.Model;

//        public IMutableEntityType BaseType { get => this.m_MutableEntityType.BaseType; set => this.m_MutableEntityType.BaseType = value; }

//        public IMutableEntityType DefiningEntityType => this.m_MutableEntityType.DefiningEntityType;

//        public bool IsKeyless { get => this.m_MutableEntityType.IsKeyless; set => this.m_MutableEntityType.IsKeyless = value; }

//        public string DefiningNavigationName => this.m_MutableEntityType.DefiningNavigationName;

//        public string Name => this.m_MutableEntityType.Name;

//        public Type ClrType => this.m_MutableEntityType.ClrType;

//        IEntityType IEntityType.BaseType => ((IEntityType)this.m_MutableEntityType).BaseType;

//        IEntityType IEntityType.DefiningEntityType => ((IEntityType)this.m_MutableEntityType).DefiningEntityType;

//        IModel ITypeBase.Model => ((ITypeBase)this.m_MutableEntityType).Model;

//        public IAnnotation AddAnnotation(string name, object value) => this.m_MutableEntityType.AddAnnotation(name, value);
//        public IMutableForeignKey AddForeignKey(IReadOnlyList<IMutableProperty> properties, IMutableKey principalKey, IMutableEntityType principalEntityType) => this.m_MutableEntityType.AddForeignKey(properties, principalKey, principalEntityType);
//        public void AddIgnored(string memberName) => this.m_MutableEntityType.AddIgnored(memberName);
//        public IMutableIndex AddIndex(IReadOnlyList<IMutableProperty> properties) => this.m_MutableEntityType.AddIndex(properties);
//        public IMutableKey AddKey(IReadOnlyList<IMutableProperty> properties) => this.m_MutableEntityType.AddKey(properties);
//        public IMutableProperty AddProperty(string name, Type propertyType, MemberInfo memberInfo)
//            => this.m_MutableEntityType.AddProperty(name, propertyType, memberInfo);
//        public IMutableServiceProperty AddServiceProperty(MemberInfo memberInfo) => this.m_MutableEntityType.AddServiceProperty(memberInfo);
//        public IAnnotation FindAnnotation(string name) => this.m_MutableEntityType.FindAnnotation(name);
//        public IMutableForeignKey FindForeignKey(IReadOnlyList<IProperty> properties, IKey principalKey, IEntityType principalEntityType) => this.m_MutableEntityType.FindForeignKey(properties, principalKey, principalEntityType);
//        public IMutableIndex FindIndex(IReadOnlyList<IProperty> properties) => this.m_MutableEntityType.FindIndex(properties);
//        public IMutableKey FindKey(IReadOnlyList<IProperty> properties) => this.m_MutableEntityType.FindKey(properties);
//        public IMutableKey FindPrimaryKey() => this.m_MutableEntityType.FindPrimaryKey();
//        public IMutableProperty FindProperty(string name) => this.m_MutableEntityType.FindProperty(name);
//        public IMutableServiceProperty FindServiceProperty(string name) => this.m_MutableEntityType.FindServiceProperty(name);
//        public IEnumerable<IAnnotation> GetAnnotations() => this.m_MutableEntityType.GetAnnotations();
//        public IEnumerable<IMutableForeignKey> GetForeignKeys() => this.m_MutableEntityType.GetForeignKeys();
//        public IReadOnlyList<string> GetIgnoredMembers() => this.m_MutableEntityType.GetIgnoredMembers();
//        public IEnumerable<IMutableIndex> GetIndexes() => this.m_MutableEntityType.GetIndexes();
//        public IEnumerable<IMutableKey> GetKeys() => this.m_MutableEntityType.GetKeys();
//        public IEnumerable<IMutableProperty> GetProperties() => this.m_MutableEntityType.GetProperties();
//        public IEnumerable<IMutableServiceProperty> GetServiceProperties() => this.m_MutableEntityType.GetServiceProperties();
//        public bool IsIgnored(string memberName) => this.m_MutableEntityType.IsIgnored(memberName);
//        public IAnnotation RemoveAnnotation(string name) => this.m_MutableEntityType.RemoveAnnotation(name);
//        public void RemoveForeignKey(IMutableForeignKey foreignKey) => this.m_MutableEntityType.RemoveForeignKey(foreignKey);
//        public void RemoveIgnored(string memberName) => this.m_MutableEntityType.RemoveIgnored(memberName);
//        public void RemoveIndex(IMutableIndex index) => this.m_MutableEntityType.RemoveIndex(index);
//        public void RemoveKey(IMutableKey key) => this.m_MutableEntityType.RemoveKey(key);
//        public void RemoveProperty(IMutableProperty property) => this.m_MutableEntityType.RemoveProperty(property);
//        public IMutableServiceProperty RemoveServiceProperty(string name) => this.m_MutableEntityType.RemoveServiceProperty(name);
//        public void SetAnnotation(string name, object value) => this.m_MutableEntityType.SetAnnotation(name, value);
//        public IMutableKey SetPrimaryKey(IReadOnlyList<IMutableProperty> properties) => this.m_MutableEntityType.SetPrimaryKey(properties);
//        IForeignKey IEntityType.FindForeignKey(IReadOnlyList<IProperty> properties, IKey principalKey, IEntityType principalEntityType) => ((IEntityType)this.m_MutableEntityType).FindForeignKey(properties, principalKey, principalEntityType);
//        IIndex IEntityType.FindIndex(IReadOnlyList<IProperty> properties) => ((IEntityType)this.m_MutableEntityType).FindIndex(properties);
//        IKey IEntityType.FindKey(IReadOnlyList<IProperty> properties) => ((IEntityType)this.m_MutableEntityType).FindKey(properties);
//        IKey IEntityType.FindPrimaryKey() => ((IEntityType)this.m_MutableEntityType).FindPrimaryKey();
//        IProperty IEntityType.FindProperty(string name) => ((IEntityType)this.m_MutableEntityType).FindProperty(name);
//        IServiceProperty IEntityType.FindServiceProperty(string name) => ((IEntityType)this.m_MutableEntityType).FindServiceProperty(name);
//        IEnumerable<IForeignKey> IEntityType.GetForeignKeys() => ((IEntityType)this.m_MutableEntityType).GetForeignKeys();
//        IEnumerable<IIndex> IEntityType.GetIndexes() => ((IEntityType)this.m_MutableEntityType).GetIndexes();
//        IEnumerable<IKey> IEntityType.GetKeys() => ((IEntityType)this.m_MutableEntityType).GetKeys();
//        IEnumerable<IProperty> IEntityType.GetProperties() => ((IEntityType)this.m_MutableEntityType).GetProperties();
//        IEnumerable<IServiceProperty> IEntityType.GetServiceProperties() => ((IEntityType)this.m_MutableEntityType).GetServiceProperties();

//    }

//}
