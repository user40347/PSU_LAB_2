open System

// Обработка вводимого натурального числа
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

// Функция для получения первой цифры числа
let rec takeFirst (x: float) =
    let x = abs (int x)
    if x < 10 then 
        x 
    else 
        takeFirst (float (x / 10))

// Основная программа
[<EntryPoint>]
let main argv =
    let n = readPositiveInt "Введите количество чисел: "
    let selectMethod = readSelectedMethod ()
    let list = createList n selectMethod
    printfn "Текущий список: %A" list

    let resultList = List.map takeFirst list
    printfn "Полученный список: %A" resultList

    0