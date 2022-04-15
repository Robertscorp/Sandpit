using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace Sandpit.SemiStaticEntity
{

    public class RelationalTypeMappingDecorator : RelationalTypeMapping
    {

        #region - - - - - - Fields - - - - - -

        private readonly Func<IAnnotation, ExpressionVisitor> m_DataReaderExpressionVisitorFactory;
        private readonly IProperty m_Property;
        private readonly RelationalTypeMapping m_RelationalTypeMapping;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public RelationalTypeMappingDecorator(RelationalTypeMapping relationalTypeMapping, IProperty property, Expression queryContextParameter)
            : base(relationalTypeMapping.StoreType,
                  relationalTypeMapping.ClrType,
                  relationalTypeMapping.DbType,
                  relationalTypeMapping.IsUnicode,
                  relationalTypeMapping.Size)
        {
            if (queryContextParameter is null)
                throw new ArgumentNullException(nameof(queryContextParameter));

            this.m_DataReaderExpressionVisitorFactory = typeMappingAnnotation => new DataReaderExpressionVisitor(queryContextParameter, typeMappingAnnotation);
            this.m_Property = property ?? throw new ArgumentNullException(nameof(property));
            this.m_RelationalTypeMapping = relationalTypeMapping ?? throw new ArgumentNullException(nameof(relationalTypeMapping));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => throw new NotImplementedException(); // TODO: Implement

        public override Expression CustomizeDataReaderExpression(Expression expression)
        {
            var _TypeMappingAnnotation = this.m_Property.FindAnnotation("StaticEntity.TypeMapping"); // TODO: Hard-coded string
            if (_TypeMappingAnnotation != null)
                expression = this.m_DataReaderExpressionVisitorFactory(_TypeMappingAnnotation).Visit(expression);

            return this.m_RelationalTypeMapping.CustomizeDataReaderExpression(expression);
        }

        #endregion Methods

    }

}
