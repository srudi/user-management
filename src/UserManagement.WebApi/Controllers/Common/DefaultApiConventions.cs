using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace UserManagement.WebAPI.Controllers.Common
{
    public static class DefaultApiConventions
    {
        //
        // Summary:
        //     Create convention.
        //
        // Parameters:
        //   model:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Create([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Delete convention.
        //
        // Parameters:
        //   id:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Edit convention.
        //
        // Parameters:
        //   id:
        //
        //   model:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Edit([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Find convention.
        //
        // Parameters:
        //   id:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Find([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Get convention.
        //
        // Parameters:
        //   id:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Get All convention.
        //
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Get All Paged convention.
        //
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)] object pageSize,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)] object pageIndex,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Post convention.
        //
        // Parameters:
        //   model:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Put convention.
        //
        // Parameters:
        //   id:
        //
        //   model:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Put([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

        //
        // Summary:
        //     Update convention.
        //
        // Parameters:
        //   id:
        //
        //   model:
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public static void Update([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)] CancellationToken cancellationToken)
        { }

    }
}
