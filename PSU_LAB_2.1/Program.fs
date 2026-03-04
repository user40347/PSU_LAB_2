open System

// Проверка вводимого целого числа
let rec readInt (prompt: string) =
    printf "%s" prompt
    match Console.ReadLine() with
    | null | "" ->
        printfn "Ошибка: введите целое число."
        readInt prompt
    | s ->
        match Int32.TryParse(s) with
        | (true, v) -> v
        | _ ->
            printfn "Ошибка: некорректный формат целого числа."
            readInt prompt

// Проверка вводимого вещественного числа
let rec readFloat (prompt: string) =
    printf "%s" prompt
    match Console.ReadLine() with
    | null | "" ->
        printfn "Ошибка: введите число."
        readFloat prompt
    | s ->
        match Double.TryParse(s) with
        | (true, v) -> v
        | _ ->
            printfn "Ошибка: некорректный формат числа."
            readFloat prompt

// Обработка вводимого положительного целого числа
let rec readPositiveInt (prompt: string) =
    printf "%s" prompt
    match Console.ReadLine() with
    | null | "" ->
        printfn "Ошибка: введите целое число."
        readPositiveInt prompt
    | s ->
        match Int32.TryParse(s) with
        | (true, v) when v > 0 -> v
        | _ ->
            printfn "Ошибка: введите число > 0."
            readPositiveInt prompt

// Выбор метода заполнения списка
let rec readSelectedMethod () =
    printfn "Выберите способ заполнения:\n1) С клавиатуры\n2) Путем заполнения случайными числами"
    match Console.ReadLine() with
    | "1" | "2" as method -> method
    | _ ->
        printfn "Такого метода нет. Попробуйте снова."
        readSelectedMethod ()

// Создание списка
let createList (n: int) (method: string) =
    let rnd = Random()
    [
        for _ in 1..n do
            if method = "1" then
                yield readFloat "Введите число: "
            elif method = "2" then
                yield (float (rnd.Next(10, 1000))) / 10.0
    ]

// Подсчет количества совпадений числа в списке
let countOfMatches (list: float list) (target: float) =
    list |> List.fold (fun acc x -> if x = target then acc + 1 else acc) 0

// Основная программа
[<EntryPoint>]
let main argv =
    let n = readPositiveInt "Введите количество чисел: "
    let selectMethod = readSelectedMethod ()
    let list = createList n selectMethod
    printfn "Текущий список: %A" list

    let compnum = readFloat "Введите число для подсчета: "
    let matches = countOfMatches list compnum
    printfn "Количество чисел %.2f в списке: %d" compnum matches

    0 