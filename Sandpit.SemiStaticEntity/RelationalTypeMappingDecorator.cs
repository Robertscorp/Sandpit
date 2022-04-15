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
            this.m_DataReaderExpressionVisitorFactory = typeMappingAnnotation => new DataReaderExpressionVisitor(queryContextParameter, typeMappingAnnotation);
            this.m_RelationalTypeMapping = relationalTypeMapping ?? throw new ArgumentNullException(nameof(relationalTypeMapping));
            this.m_Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override Expression CustomizeDataReaderExpression(Expression expression)
        {
            var _TypeMappingAnnotation = this.m_Property.FindAnnotation("StaticEntity.TypeMapping");
            if (_TypeMappingAnnotation != null)
            {
                //var _TypeMapping = Expression.Constant(_TypeMappingAnnotation.Value);
                //var _DbContext = Expression.Property(this.m_QueryContextParameter, nameof(QueryContext.Context));
                //var _ToModel = Expression.Property(_TypeMapping, "ToModel");

                //var _X = _TypeMapping.Type.GenericTypeArguments[1];
                //var _X2 = expression.

                //xxx // The expression parameter is returning a "SemiStaticEntity" as it's type, even though it's a call to
                // data reader. "expression" will need to be re-written...

                expression = this.m_DataReaderExpressionVisitorFactory(_TypeMappingAnnotation).Visit(expression);

                //expression = expression.
                //expression = Expression.Invoke(_ToModel, _DbContext, expression);
            }

            return this.m_RelationalTypeMapping.CustomizeDataReaderExpression(expression);
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => throw new System.NotImplementedException();

        #endregion Methods

    }

}
