- title : F# Gospel
- description : Introduction to F#
- author : Guido
- theme : night
- transition : default

***

### F# Gospel Chord Progression

[![](http://img.youtube.com/vi/CojCWjVrvKg/0.jpg)](https://www.youtube.com/watch?v=CojCWjVrvKg "F# chords")

***

### F# F31day thaks to FsReveal

- Generates [reveal.js](http://lab.hakim.se/reveal-js/#/) presentation from [markdown](http://daringfireball.net/projects/markdown/)
- Utilizes [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) for markdown parsing
- Get it from [http://fsprojects.github.io/FsReveal/](http://fsprojects.github.io/FsReveal/)

![FsReveal](images/logo.png)

*** 

### Before I get lost

#### How have I met F#?

- First things [Fable](https://fable.io/) (mappa) (anche pandemic in the world)
- very nice channel to start with Fable and more [(òvò) the dev owl](https://www.youtube.com/channel/UCOX5DkLyqctM-wkOAU_mUpA) 
- Idiomatic functional tecniques
- Using type system to describe computation (Hacker Rank)
- Another purity/honesty and all that, no code really mostly a talk [Erik Meyer](https://channel9.msdn.com/Shows/Going+Deep/Erik-Meijer-Functional-Programming)
- check [F# advent calendar](https://sergeytihon.com/2020/10/22/f-advent-calendar-in-english-2020/) [original map](http://www.markpattison.net/mapdemo/)

***

### F# a functional first programming Language

- Family tree [ML](https://it.wikipedia.org/wiki/ML)
- A different type system/compiler interaction
- [Hindley–Milner type system](https://en.wikipedia.org/wiki/Hindley%E2%80%93Milner_type_system)
- [F# Don Syme](https://www.youtube.com/watch?v=yL7xBhWrdKw)

***
### A very first glance

#### let the compiler read your mind 


    let a = 5
    let factorial x = [1..x] |> List.reduce (*)
    let c = factorial a

---


### Let's double check 

#### type system and compiler working togheter

	type Aggregate = { Name: string; Age:int}
	let print a = sprintf "%s %i" a.Name a.Age 


- code oragnization requirements..
	
---

### Function signature

#### Most important feature to get used to

- any function is a function of one single argument 
- curry uncurry briefly



    let f x = 1 + x
    let sum x y = x + y
    //not the same
    let add (x,y) = x + y

---

### Pipe Operator
	
#### a very idiomatic F# syntax

	let square x = x * x
	//pipe operator 
	let x = 4 |> square
	
---

### Discriminated Unions

#### an easy building block 

- this something not so easy to reproduce in OO
- .. it gets quite verbose


	let Partnumber = Partnumber string // single case	
	let Contact = Phone string | CellPhone string
	
		//defining a Maybe from scracth
	type Maybe<'a> =
		| Just of 'a
		| BadLuck
*** 

### Many Many more features

#### usually code is much shorter

- Pattern Matching
- Computation Expressions
- Different Collection environment
- Remark: F# list is not C# list!
- Records all over the place


---

### Maybe we should take a look

#### Achive compoistion!
- composition is day zero target [many many more](file://./Scripts/ManyManyMore.fsx) 
- [Google Haskell 101](https://www.youtube.com/watch?v=cTN1Qar4HSw)
- Many times you will find links to *Haskell* and *Scala*
- Might seem too much but I think it's a good indeed


		
*** 


### Enough! 

#### time for some scripting

- let's check some simple lines [basic idioms showcase](file://./Scripts/Basic-Idioms.fsx)

---

### Moving to something more realworld-ish

#### let's play with Dataproviders

- time to read some data from the network
- and maybe face our first computation expression

---

### and now some Corefront interaction

#### yet again

	url cmsApiUrl context cartLabel
	|> Seq.toArray
	|> Array.map Http.AsyncRequestString
	|> Async.Parallel
	|> Async.RunSynchronously

***

### Monads

#### You are going to meet them anyway

#### 3 basic idioms: *map, bind, apply* 

		(a -> b) -> Ma -> Mb	//map <!>
		(a -> Mb) -> Ma -> Mb	//bind  >>=
		M(a->b) -> Ma -> Mb 	//apply <*>
	
- Not really that complex
- Key Idea: composing functions
- sometimes names differ a little
- funny [The fisrt monad tutorial](https://www.youtube.com/watch?v=yjmKMhJOJos) by Lambda-Man
- very easy [no mess](https://www.youtube.com/watch?v=c8eCE1Yrolc&t=1s)
- [Railway oriented programming](https://vimeo.com/97344498) or 'the elevated world'



***
### Validazione 

#### functional validation

- applicative [resultScript](file://./Scripts/resultScript.fsx)



	let makeCustomer name email phone =
		createCustomer 
		<!> validateName name
		<*> validateEmail email
		<*> validatePhone phone

***

### Monad Transformer

#### some times it is not so easy

- Monad do not compose! 

![monad tranformer](https://miro.medium.com/max/500/1*Tb2VjzXOJiXImWZymmDCfQ.jpeg)


***
	
### Kleisli composition

- composing *(a -> Mb)*  and *(b -> Mc)*
- N.B. *(>>=)* è il bind


	//Kleisli composition or fish operator
	let (>=>) f1 f2 arg =
		f1 arg >>= f2

![the fishy operator](https://images.unsplash.com/photo-1508886661276-2ba595d8cd22?w=1226&h=400&fit=crop&crop=edges)


***

### A glace at web frameworks

- Giraffe, Saturn, Suave (l'idea originale) [Giraffe](https://giraffe.wiki/docs#httphandler) [Saturn](https://saturnframework.org/explanations/overview.html) [Suave ](https://suave.io/) [Safe](https://safe-stack.github.io/docs/template-overview/)
- Pipeline and Kleisli at work


	let app =
		choose [
			route "/foo" >=> text "Foo"
			route "/bar" >=> text "Bar"
		]


	let app =
		route "/"
		>=> setHttpHeader "X-Foo" "Bar"
		>=> setStatusCode 200
		>=> setBodyFromString "Hello World"
		


***
### And finally we are done

#### thank you so much

##### time for some more F# music ^^
[![](http://img.youtube.com/vi/TpAmXxXGNLI/0.jpg)](http://www.youtube.com/watch?v=TpAmXxXGNLI "F.J. Haydn - Sinfonia n° 45 degli Addii - Finale: Presto")

