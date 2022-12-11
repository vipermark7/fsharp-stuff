open Lit
open Lit.Elmish
open LitStore
open LitRouter
open Fable.Remoting.Client
open Shared

let remoteApi =
  Remoting.createApi ()
  |> Remoting.withBaseUrl "/api"
  |> Remoting.buildProxy<IServerApi>

type State = {Value : string}

type Msg =
    | SetValue of string
    | GetValueFromServer

// let fakeCallToServer () = async {
//     do! Async.Sleep(3000)
//     return "This is a value from the server"
// }

let init () = {Value = ""}, Elmish.Cmd.none

let update (msg: Msg) (state: State) =
    match msg with
    | SetValue value -> {state with Value = value}, Elmish.Cmd.none
    | GetValueFromServer -> state, Elmish.Cmd.OfAsync.perform remoteApi.Ping () SetValue

[<LitElement("my-app")>]
let MyApp () =
    let _ = LitElement.init (fun cfg -> cfg.useShadowDom <- false)
    let state, dispatch = Hook.useElmish(init, update)

    html
        $"""
        <p>{state.Value}</p>
        <input></input>
        <button @click={Ev(fun _ -> dispatch GetValueFromServer)}>GetValueFromServer</button>
        """
