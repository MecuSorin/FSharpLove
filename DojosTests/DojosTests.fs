namespace DojosTests

open Xunit
open System


module DojosTests =

    [<Fact>]
    let ``Test a fact to verify the testing setup`` () =
        let diff a b  = a - b
        Assert.Equal(1, diff 2 1)

    [<Theory>]
    [<InlineData(3)>]
    let ``Test a theory to verify the testing setup`` s =
        Assert.Equal(3, s)

    [<Fact>]
    let ``Test exception throw to verify the testing setup`` () =
        let msg = "Custom exc"
        let exceptionThrower () = raise (ApplicationException(msg))
        // simplified version
        Assert.Throws<ApplicationException>(Action exceptionThrower) |> ignore
        // true BDD version
        let exc = Record.Exception(Action exceptionThrower)
        Assert.NotNull(exc)
        Assert.Equal(msg, exc.Message)

    [<FsCheck.Xunit.Property>]
    let ``Test integration between FsCheck and xUnit to verify the testing setup`` a b =
        let diff a b  = a - b
        diff a b = a - b
