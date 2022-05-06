using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;

namespace Sandpit.SemiStaticEntity
{

    public class SelectExpressionVisitor
    {

        public SelectExpression Visit(SelectExpression node)
        {
            var _X = (List<ProjectionExpression>)node.Projection;

            return node;
        }

    }

}
