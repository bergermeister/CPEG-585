import numpy as voNP
import cv2
from matplotlib import pyplot as voPlot

# Load a color image in grayscale
# voImg = cv2.imread( "Resources/obama1.jpg", 1 ) # 0 for gray, 1 for color

# canny edge detection
voImg = cv2.imread( "Resources/obama1.jpg", 0 )
voEdges = cv2.Canny( voImg, 50, 200 )

voPlot.subplot( 121 ), voPlot.imshow( voImg, cmap = 'gray' )
voPlot.title( 'Original Image' ), voPlot.xticks([]), voPlot.yticks([])
voPlot.subplot( 122 ), voPlot.imshow( voEdges, cmap = 'gray' )
voPlot.title( 'Edge Image' ), voPlot.xticks([]), voPlot.yticks([])
voPlot.show( )

# Harris Corner Detection
voImg = cv2.imread( "Resources/Chessboard.jpg" )
voGray = cv2.cvtColor( voImg, cv2.COLOR_BGR2GRAY )

# find Harris corners
voGray = voNP.float32( voGray )
voDst = cv2.cornerHarris( voGray, 2, 3, 0.04 )

# Result is dilated for marking the corners, not important
voDst = cv2.dilate( voDst, None )

# Threshold for an optimal value, it may vary depending on the image.
voImg[ voDst > 0.01 * voDst.max( ) ] = [ 0, 0, 255 ]

cv2.imshow( 'dst', voImg )
cv2.waitKey( 0 )
cv2.destroyAllWindows( )
