import sys
import numpy as voNP
import matplotlib.pyplot as voPlot

def main( ) :
    kdX = voNP.ndarray( ( 6, 1 ) ) # X Coordinates
    kdY = voNP.ndarray( ( 6, 1 ) ) # Y Coordinates
    kdC = voNP.ndarray( ( 4, 4 ) )      # Coefficient Matrix
    kdR = voNP.ndarray( ( 4, 1 ) )      # Result Matrix

    # Define X coordinates
    kdX[ 0 ] = [ 1 ]
    kdX[ 1 ] = [ 2 ]
    kdX[ 2 ] = [ 3 ]
    kdX[ 3 ] = [ 4 ]
    kdX[ 4 ] = [ 5 ]
    kdX[ 5 ] = [ 6 ]

    # Define Y coordinates
    kdY[ 0 ] = [  -0.6 ]
    kdY[ 1 ] = [   8.3 ]
    kdY[ 2 ] = [  26.0 ]
    kdY[ 3 ] = [  57.0 ]
    kdY[ 4 ] = [ 108.0 ]
    kdY[ 5 ] = [ 173.0 ]

    # Initialize Coefficient and Result Matrices to 0
    for kiRow in range( 0, 4, 1 ) :
        kdR[ kiRow, 0 ] = 0.0
        for kiCol in range( 0, 4, 1 ) :
            kdC[ kiRow, kiCol ] = 0.0

    for kiDC in range( 0, 4, 1 ) :
        for kiIdx in range( 0, 6, 1 ) :
            kdC[ kiDC ][ 0 ] += 2 * ( kdX[ kiIdx ] ** ( 6 - kiDC ) )                # 2ax^6, 2ax^5, 2ax^4, 2ax^3
            kdC[ kiDC ][ 1 ] += 2 * ( kdX[ kiIdx ] ** ( 5 - kiDC ) )                # 2bx^5, 2bx^4, 2bx^3, 2bx^2
            kdC[ kiDC ][ 2 ] += 2 * ( kdX[ kiIdx ] ** ( 4 - kiDC ) )                # 2cx^4, 2cx^3, 2cx^2, 2cx^1
            kdC[ kiDC ][ 3 ] += 2 * ( kdX[ kiIdx ] ** ( 3 - kiDC ) )                # 2dx^3, 2dx^2, 2dx^1, 2d
            kdR[ kiDC ][ 0 ] += 2 * ( kdX[ kiIdx ] ** ( 3 - kiDC ) ) * kdY[ kiIdx ] # 2yx^3, 2yx^2, 2yx^1, 2y

    # Compute the inverse of the Coefficient Matrix:
    kdCi = voNP.linalg.inv( kdC )

    # Compute the Coefficient Vector by taking the dot product of the Coefficient Matrix with the Result Matrix
    kdM = voNP.dot( kdCi, kdR )

    # Print the Coefficients
    print( "a = ", kdM[ 0, 0 ] )
    print( "b = ", kdM[ 1, 0 ] )
    print( "c = ", kdM[ 2, 0 ] )
    print( "d = ", kdM[ 3, 0 ] )

    # do a scatter plot of the data
    kiArea = 3
    koColors =['black']
    voPlot.scatter( kdX, kdY, s = kiArea, c = koColors, alpha = 0.5, linewidths = 8 )
    voPlot.title( 'Linear Least Squares Regression' )
    voPlot.xlabel( 'x' )
    voPlot.ylabel( 'y' )

    #plot the fitted line
    kdFitted = ( kdX ** 3 ) * kdM[ 0, 0 ] + ( kdX ** 2 ) * kdM[ 1, 0 ] + ( kdX ) * kdM[ 2, 0 ] + kdM[ 3, 0 ]
    koLine,=voPlot.plot( kdX, kdFitted, '--', linewidth = 2 ) #line plot
    koLine.set_color( 'red' )
    voPlot.show( )

if __name__ == "__main__" :
    main( )
