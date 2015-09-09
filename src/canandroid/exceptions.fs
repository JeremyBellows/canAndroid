namespace canAndroid 

    module exceptions =
        open System

        type CanAndroidException(message) = inherit Exception(message)
        type CanAndroidInsistFailedException(message) = inherit CanAndroidException(message)

