namespace Fdalilib
{
    /// <summary>
    ///     Служит для представления возвращаемого значения, если требуется обеспечить возврат объекта одного из двух типов в зависимости от результата вызова
    /// </summary>
    /// <typeparam name="T">Ожидаемый тип возвращаемого значения</typeparam>
    /// <typeparam name="TErr">Тип с описанием ошибки</typeparam>
    public struct Answer<T, TErr> where T : class where TErr : class
    {
        private readonly object _result;

        /// <summary>
        ///     Создаёт экземпляр Answer
        /// </summary>
        /// <param name="result">Объект, который вернул метод</param>
        public Answer(object result)
        {
            _result = result;
        }

        /// <summary>
        ///     Получает объект, возвращённый методом
        /// </summary>
        public object RawObject
        {
            get { return _result; }
        }

        /// <summary>
        ///     В случае удачного вызова метода это свойство содержит объект ожидаемого типа
        /// </summary>
        public T Result
        {
            get { return _result as T; }
        }

        /// <summary>
        ///     В случае, если метод вернул ошибку, это свойство содержит объект типа TErr с описанием ошибки
        /// </summary>
        public TErr Error
        {
            get { return _result as TErr; }
        }

        /// <summary>
        ///     Получает признак удачного вызова метода
        /// </summary>
        public bool IsSucceded
        {
            get { return _result is T; }
        }

        /// <summary>
        ///     Получает признак, что результат метода вернул объект типа, отличного от ожидаемого или типа ошибки
        /// </summary>
        public bool IsUnexpectedResult
        {
            get { return !(_result is TErr || _result is T); }
        }
    }
}