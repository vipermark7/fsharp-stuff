module Shared

type IServerApi = { Ping: unit -> Async<string> }
