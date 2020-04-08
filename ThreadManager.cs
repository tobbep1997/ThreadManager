using System;
using System.Threading;

namespace ThreadManager
{
    /// <summary>
    /// Class to create async threads
    /// </summary>
    public static class ThreadManager
    {
        private class IThread
        {
            public Thread Thread = null;

            public IThread(
                string ThreadName,
                Func<int> method,
                Func<int> externalAsyncCallback = null)
            {
                this.Thread = new Thread(() => ExecuteThread(
                    method, externalAsyncCallback))
                {
                    Name = ThreadName
                };
                this.Thread.Start();
            }

            public IThread(
                string ThreadName,
                Func<int> method,
                Func<int> externalAsyncCallback,
                Func<Exception, int> externalAsyncCallbackException)
            {
                this.Thread = new Thread(() => ExecuteThreadExceptionHandeling(
                    method, externalAsyncCallback, externalAsyncCallbackException))
                {
                    Name = ThreadName
                };
                this.Thread.Start();
            }

            public IThread(
                string ThreadName,
                Func<object, int> method,
                object methodObj,
                Func<object, int> externalAsyncCallBack,
                object externalAsyncCallBackObj,
                Func<Exception, int> externalAsyncCallbackException)
            {
                this.Thread = new Thread(() => ExecuteThreadParameterizedExceptionHandeling(
                    method, methodObj, externalAsyncCallBack, externalAsyncCallBackObj, externalAsyncCallbackException))
                {
                    Name = ThreadName
                };
                this.Thread.Start();
            }

            public IThread(
                string ThreadName,
                Func<object, int> method,
                object obj,
                Func<object, int> externalAsyncCallback,
                object callbackObj)
            {
                this.Thread = new Thread(() => ExecuteThreadParameterized(
                    method, obj, externalAsyncCallback, callbackObj))
                {
                    Name = ThreadName
                };
                this.Thread.Start();
            }
        }

        //public functions 

        /// <summary>
        /// Creates a thread that is executed asyncronasly.
        /// OBS All functions must return an int but it will only be evaluated for the "Method".
        /// </summary>
        /// <param name="ThreadName">Name of the thread. Is used for debugging puroposes. </param>
        /// <param name="Method">The method that the thread will execute. </param>
        /// <param name="ExternalAsyncCallback">If defined the callback function will be called if the method returns 0. null is allowed. </param>
        public static void AddThread(
            string ThreadName,
            Func<int> Method,
            Func<int> ExternalAsyncCallback)
        {
            if (string.IsNullOrEmpty(ThreadName))
            {
                throw new ArgumentException("message", nameof(ThreadName));
            }

            if (Method is null)
            {
                throw new ArgumentNullException(nameof(Method));
            }

            _ = new IThread(ThreadName, Method, ExternalAsyncCallback);
        }



        /// <summary>
        /// Creates a thread that is executed asyncronasly with exception handeling.   
        /// OBS All functions must return an int but it will only be evaluated for the "Method".
        /// </summary>
        /// <param name="ThreadName">Name of the thread. Is used for debugging puroposes. </param>
        /// <param name="Method">The method that the thread will execute. </param>
        /// <param name="ExternalAsyncCallback">If defined the callback function will be called if the "Method" returns 0. null is allowed. </param>
        /// <param name="ExternalAsyncCallbackException">If defined the callback function will be called if the "Method" throws an error. null is allowed. </param>
        public static void AddThreadExceptionHandeling(
            string ThreadName,
            Func<int> Method,
            Func<int> ExternalAsyncCallback,
            Func<Exception, int> ExternalAsyncCallbackException)
        {
            if (string.IsNullOrEmpty(ThreadName))
            {
                throw new ArgumentException("message", nameof(ThreadName));
            }

            if (Method is null)
            {
                throw new ArgumentNullException(nameof(Method));
            }

            _ = new IThread(ThreadName, Method, ExternalAsyncCallback, ExternalAsyncCallbackException);
        }



        /// <summary>
        /// Creates a thread that is executed asyncronasly with paramters.   
        /// OBS All functions must return an int but it will only be evaluated for the "Method".
        /// </summary>
        /// <param name="ThreadName">Name of the thread. Is used for debugging puroposes. </param>
        /// <param name="Method">The method that the thread will execute. </param>
        /// <param name="MethodObj">Object to be passed as a paramter to the "Method" function. </param>
        /// <param name="ExternalAsyncCallback">If defined the callback function will be called if the "Method" returns 0. null is allowed. </param>
        /// <param name="ExternalAsyncCallBackObj">Object to be passed as a paramter to the "ExternalAsyncCallback" function. Null is allowed. </param>
        public static void AddThreadParameterized(
            string ThreadName,
            Func<object, int> Method,
            object MethodObj,
            Func<object, int> ExternalAsyncCallback,
            object ExternalAsyncCallBackObj)
        {
            if (string.IsNullOrEmpty(ThreadName))
            {
                throw new ArgumentException("message", nameof(ThreadName));
            }

            if (Method is null)
            {
                throw new ArgumentNullException(nameof(Method));
            }

            if (MethodObj is null)
            {
                throw new ArgumentNullException(nameof(MethodObj));
            }

            _ = new IThread(ThreadName, Method, MethodObj, ExternalAsyncCallback, ExternalAsyncCallBackObj);
        }



        /// <summary>
        /// Creates a thread that is executed asyncronasly with parameters and exception handeling.   
        /// OBS All functions must return an int but it will only be evaluated for the "Method".
        /// </summary>
        /// <param name="ThreadName">Name of the thread. Is used for debugging puroposes. </param>
        /// <param name="Method">The method that the thread will execute. </param>
        /// <param name="MethodObj">Object to be passed as a paramter to the "Method" function. </param>
        /// <param name="ExternalAsyncCallBack">If defined the callback function will be called if the "Method" returns 0. null is allowed. </param>
        /// <param name="ExternalAsyncCallBackObj">Object to be passed as a paramter to the "ExternalAsyncCallback" function. Null is allowed. </param>
        /// <param name="ExternalAsyncCallbackException">If defined the callback function will be called if the "Method" throws an error. null is allowed. </param>
        public static void AddThreadParameterizedExceptionHandeling(
            string ThreadName,
            Func<object, int> Method,
            object MethodObj,
            Func<object, int> ExternalAsyncCallBack,
            object ExternalAsyncCallBackObj,
            Func<Exception, int> ExternalAsyncCallbackException)
        {
            if (string.IsNullOrEmpty(ThreadName))
            {
                throw new ArgumentException("message", nameof(ThreadName));
            }

            if (Method is null)
            {
                throw new ArgumentNullException(nameof(Method));
            }

            if (MethodObj is null)
            {
                throw new ArgumentNullException(nameof(MethodObj));
            }

            _ = new IThread(ThreadName, Method, MethodObj, ExternalAsyncCallBack, ExternalAsyncCallBackObj, ExternalAsyncCallbackException);
        }

        //private ---------------------------------------------------------------------------------------------------------

        private static void ExecuteThread(
            Func<int> method,
            Func<int> externalAsyncCallback)
        {
            if (method() == 0)
            {
                _ = externalAsyncCallback?.Invoke();
            }
        }

        private static void ExecuteThreadExceptionHandeling(
            Func<int> method,
            Func<int> externalAsyncCallback,
            Func<Exception, int> externalAsyncCallbackException)
        {
            try
            {
                _ = method();
                _ = externalAsyncCallback?.Invoke();
            }
            catch (Exception exception)
            {
                _ = externalAsyncCallbackException?.Invoke(exception);
            }
        }

        private static void ExecuteThreadParameterized(
            Func<object, int> method,
            object obj,
            Func<object, int> externalAsyncCallback,
            object callbackObj)
        {
            if (method(obj) == 0)
            {
                _ = externalAsyncCallback?.Invoke(callbackObj);
            }
        }
        private static void ExecuteThreadParameterizedExceptionHandeling(
            Func<object, int> method,
            object methodObj,
            Func<object, int> externalAsyncCallBack,
            object externalAsyncCallBackObj,
            Func<Exception, int> externalAsyncCallbackException)
        {
            try
            {
                _ = method(methodObj);
                _ = externalAsyncCallBack?.Invoke(externalAsyncCallBackObj);
            }
            catch (Exception exeption)
            {
                _ = externalAsyncCallbackException?.Invoke(exeption);

            }
        }
    }
}
