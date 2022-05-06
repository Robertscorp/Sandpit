//using Microsoft.EntityFrameworkCore.Query;
//using Microsoft.EntityFrameworkCore.Query.Internal;
//using System.Diagnostics.CodeAnalysis;

//namespace Sandpit.SemiStaticEntity
//{

//    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
//    public class QueryCompilationContextFactoryDecorator : IQueryCompilationContextFactory
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IQueryCompilationContextFactory m_QueryCompilationContextFactory;

//        #endregion Fields

//        #region - - - - - - Constructors - - - - - -

//        public QueryCompilationContextFactoryDecorator(
//            QueryCompilationContextDependencies dependencies)
//            => this.m_QueryCompilationContextFactory
//                = new QueryCompilationContextFactory(dependencies.With(new ModelDecorator(dependencies.Model)));

//        #endregion Constructors

//        #region - - - - - - Methods - - - - - -

//        public QueryCompilationContext Create(bool async)
//            => this.m_QueryCompilationContextFactory.Create(async);

//        #endregion Methods

//    }

//}
